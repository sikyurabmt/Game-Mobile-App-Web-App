import VPlay 2.0
import QtQuick 2.0

EntityBase {
    id: player2
    entityType: "player2"

    MovementAnimation {
        target: parentd
        property: "pos"
        velocity: Qt.point(-50, 0)
        running: true
    }
    BoxCollider{
        id: aaa
        anchors.fill: bird

        //collisionTestingOnlyMode: false
        fixture.onBeginContact: {
            var collidedEntity = other.getBody().target
            console.debug("collided with entity", collidedEntity.entityType)

            if(collidedEntity.entityType === "player") {
                bird.name="pipe"
                bird.i=1

            }
        }
    }
    SpriteSequenceVPlay {
        id: bird
        property var name: "bird"
        anchors.centerIn: parent
        property var i:3


        spriteSheetSource: "../assets/" + name + ".png"
        SpriteVPlay {
            name:a
            frameCount: i
            frameRate: 10
            frameWidth: 34
            frameHeight: 24
            //source:"../assets/bird.png"

        }

    }
}
