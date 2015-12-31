import VPlay 2.0
import QtQuick 2.0

EntityBase {
    id: player
    entityType: "player"

//    MovementAnimation {
//        target: parent
//        property: "pos"
//        velocity: Qt.point(20, 0)
//        running: true
//    }

//    BoxCollider {
//        id:aaa
//        anchors{
//            left: spritesequencePlayer.x
//            top: spritesequencePlayer.y
//        }
//    }

    SpriteSequenceVPlay {
        id: spritesequencePlayer
        x: 50
        y: 400

        SpriteVPlay {
            frameCount: 1
            frameRate: 10
            frameWidth: 104
            frameHeight: 71
            source: "../assets/players/player.png"
        }
    }
}
