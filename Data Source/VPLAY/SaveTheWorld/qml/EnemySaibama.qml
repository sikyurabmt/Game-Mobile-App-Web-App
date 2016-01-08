import VPlay 2.0
import QtQuick 2.0
import QtMultimedia 5.5

EntityBase {
    entityType: "saibama"
    id: saibama

    SpriteSequenceVPlay {
        id: saibamaImage

        SpriteVPlay {
            frameCount: 6
            frameRate: 8
            frameWidth: 64
            frameHeight: 37
            source: "../assets/enemies/saibama/saibama.png"
        }
    }

    y: utils.generateRandomValueBetween(0, rtgGamePlay.height-saibamaImage.height)

    NumberAnimation on x {
        from: rtgGamePlay.width
        to: -200 //ra khoi man hinh left
        duration: utils.generateRandomValueBetween(5000, 8000)
    }

    BoxCollider {
        anchors.fill: saibamaImage
        collisionTestingOnlyMode: true // use Box2D only for collision detection, move the entity with the NumberAnimation above
        fixture.onBeginContact: {

            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "player"){
                saibamaAttack.play()
                removeEntity()
            }
            if(collidedEntity.entityType === "skillPlayer") {
                collidedEntity.removeEntity() //xoa dan
                removeEntity() //xoa enemy
            }
            if(collidedEntity.entityType === "skillAuraBlast" || collidedEntity.entityType === "skillKamehameha") {
                removeEntity() //xoa enemy
            }
        }
    }
    Timer {
        running: true
        interval: 100
        repeat: true
        onTriggered: {
            if(saibama.x <= rtgGamePlay.x*1.05)
            {
                removeEntity()
            }
        }
    }
    MediaPlayer { id: saibamaAttack; source: "../assets/sounds/saibama/attack.wav" }
}
