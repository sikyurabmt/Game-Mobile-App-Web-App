import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "cellskill1"
    id: cellskill1
    width: 33
    height: 40

    SpriteSequenceVPlay {
        id: skillImage

        SpriteVPlay {
            frameCount: 2
            frameRate: 5
            frameWidth: 24
            frameHeight: 28
            source: "../assets/enemies/cell/cell_skill_spiritblast.png"
        }
    }

    property point appear
    property point destination

    PropertyAnimation on x {
        from: appear.x
        to: destination.x
        duration: 1000
    }

    PropertyAnimation on y {
        from: appear.y
        to: destination.y + 20
        duration: 1000
    }

    BoxCollider {
        anchors.fill: skillImage
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
