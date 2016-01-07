import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "skillAuraBlast"
    id: instanceSkill
    width: 33
    height: 40

    SpriteSequenceVPlay {
        id: skillImage

        SpriteVPlay {
            frameCount: 4
            frameRate: 10
            frameWidth: 33
            frameHeight: 40
            source: "../assets/skills/aurablast.png"
            to: {"end":1}
        }
        SpriteVPlay {
            name: "end"
            startFrameColumn: 5
            frameWidth: 33
            frameHeight: 40
            source: "../assets/skills/aurablast.png"
        }
    }

    property point destination
    property int moveDuration

    PropertyAnimation on x {
        from: player.directX
        to: destination.x
        duration: moveDuration
    }

    PropertyAnimation on y {
        from: player.directY
        to: destination.y
        duration: moveDuration
    }

    BoxCollider {
        anchors.fill: skillImage
    }
    Timer {
        running: true
        interval: 100
        repeat: true
        onTriggered: {
            if(instanceSkill.x + instanceSkill.width > rtgGamePlay.width*0.8)
            {
                removeEntity()
            }
        }
    }
}
