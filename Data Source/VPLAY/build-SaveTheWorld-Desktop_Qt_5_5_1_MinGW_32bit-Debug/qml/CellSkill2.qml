import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "cellskill2"
    id: cellskill2
    width: 33
    height: 40

    SpriteSequenceVPlay {
        id: skillImage

        SpriteVPlay {
            frameCount: 13
            frameRate: 10
            frameWidth: 96
            frameHeight: 96
            source: "../assets/enemies/cell/cell_skill_bigbang.png"
        }
    }

    property point appear
    property point destination

    PropertyAnimation on x {
        from: appear.x - 50
        to: destination.x
        duration: 1000
    }

    PropertyAnimation on y {
        from: appear.y
        to: destination.y - 20
        duration: 1000
    }

    BoxCollider {
        anchors.fill: skillImage
        fixture.onBeginContact: {
            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer" || collidedEntity.entityType === "skillAuraBlast") {
                collidedEntity.removeEntity() //xoa dan
            }
            if(collidedEntity.entityType === "skillKamehameha") {
                removeEntity() //xoa enemy
            }
        }
    }
    Timer {
        running: true
        interval: 100
        repeat: true
        onTriggered: {
            if(cellskill1.x <= rtgGamePlay.x*1.2)
            {
                removeEntity()
            }
        }
    }
}
