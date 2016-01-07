import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "monster"

    SpriteSequenceVPlay {
        id: monsterImage

        SpriteVPlay {
            name: "live"
            frameCount: 1
            frameRate: 10
            frameWidth: 27
            frameHeight: 40
            source: "../assets/enemies/Target.png"
        }
    }

    y: utils.generateRandomValueBetween(0, rtgGamePlay.height-monsterImage.height)

    NumberAnimation on x {
        from: scene.width
        to: -200 //ra khoi man hinh left
        duration: utils.generateRandomValueBetween(5000, 8000)
    }

    BoxCollider {
        anchors.fill: monsterImage
        collisionTestingOnlyMode: true // use Box2D only for collision detection, move the entity with the NumberAnimation above
        fixture.onBeginContact: {

            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer") {
                collidedEntity.removeEntity() //xoa dan
                removeEntity() //xoa enemy
            }
            if(collidedEntity.entityType === "skillAuraBlast" || collidedEntity.entityType === "skillKamehameha") {
                removeEntity() //xoa enemy
            }
        }
    }
}
