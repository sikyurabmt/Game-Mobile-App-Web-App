import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "skillPlayer"
    id: instanceSkill
    width: 42
    height: 16
    SpriteSequenceVPlay {
        id: skillImage

        SpriteVPlay {
            frameCount: 2
            frameRate: 5
            frameWidth: 42
            frameHeight: 16
            source: "../assets/skills/spiritblast.png"
        }
    }
    // these values can then be set when a new projectile is created in the MouseArea below

    property point destination
    property int moveDuration

    PropertyAnimation on x {
        from: player.directX
        to: destination.x*1.2 //cho thoat khoi man hinh
        duration: moveDuration
    }

    PropertyAnimation on y {
        from: player.directY
        to: destination.y
        duration: moveDuration
    }

    BoxCollider {
        anchors.fill: skillImage
        //collisionTestingOnlyMode: true
    }
    Timer {
        running: true
        interval: 100
        repeat: true
        onTriggered: {
            if(instanceSkill.x + instanceSkill.width >= rtgGamePlay.width*1.1 || instanceSkill.y + instanceSkill.height >= rtgGamePlay.height*0.95 || instanceSkill.y <= rtgGamePlay.y)
            {
                removeEntity()
            }
        }
    }
}// EntityBase
