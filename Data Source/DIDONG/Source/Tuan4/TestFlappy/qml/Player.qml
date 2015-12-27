import VPlay 2.0
import QtQuick 2.0

EntityBase {
    id: player
    entityType: "player"

MovementAnimation {
              target: parent
              property: "pos"
              velocity: Qt.point(20, 0)
              running: true
            }
    BoxCollider {
        id:aaa
        anchors.fill: bird // make the collider as big as the image

        //collisionTestingOnlyMode: true // use Box2D only for collision detection, move the entity with the NumberAnimation above

    }

    SpriteSequenceVPlay {
        id: bird
        anchors.centerIn: parent

        SpriteVPlay {
            frameCount: 3
            frameRate: 10
            frameWidth: 34
            frameHeight: 24
            source: "../assets/bird.png"
        }
    }
}
