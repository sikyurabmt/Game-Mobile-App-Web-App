import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "skillKamehameha"
id: instanceSkill
    SpriteSequenceVPlay {
        id: skillImage

        SpriteVPlay {
            frameCount: 3
            frameRate: 8
            frameWidth: 400
            frameHeight: 77
            source: "../assets/skills/kame.png"
        }
    }

    PropertyAnimation on x {
        from: player.directX
        to: player.directX+1
    }

    PropertyAnimation on y {
        from: player.y-11
        to: player.y-11
    }

    BoxCollider {
        anchors.fill: skillImage
    }
    Timer {
        running: true
        repeat: true
        interval: 600 // a new target(=monster) is spawned every second
        onTriggered:
            removeEntity()
    }
}

