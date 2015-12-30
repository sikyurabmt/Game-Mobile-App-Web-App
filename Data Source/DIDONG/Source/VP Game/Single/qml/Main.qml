import QtQuick 2.2
import QtQuick.Controls 1.1
import VPlay 2.0
import QtMultimedia 5.0

GameWindow {
    id: gameWindow

    activeScene: scene
    width: 1000;
    height: 600;
    visible: true;

    Rectangle {
        id: root

        width: 1000;
        height: 600;

        World { id: physicsWorld }

//        Component {
//            id: link
//            Rectangle {
//                id: chainBall

//                width: 20
//                height: 20
//                x: 400
//                color: "orange"
//                radius: 10

//                property Body body: circleBody

//                CircleBody {
//                    id: circleBody
//                    target: chainBall
//                    world: physicsWorld

//                    bodyType: Body.Dynamic
//                    radius: 10
//                    friction: 0.9
//                    density: 0.0008
//                }
//            }
//        }

//        Component {
//            id: linkJoint
//            RevoluteJoint {
//                localAnchorA: Qt.point(10, 28)
//                localAnchorB: Qt.point(10, 5)
//                collideConnected: true
//            }
//        }

        Component {
            id: ball
            Rectangle {
                id: bulletBall

                width: 10
                height: 10
                radius: 5
                color: "black"
                smooth: true

                property Body body: circleBody

                CircleBody {
                    id: circleBody

                    target: bulletBall
                    world: physicsWorld

                    bullet: true
                    bodyType: Body.Dynamic

                    radius: 5
                    density: 0.0009
                    friction: 0.9
                    restitution: 0.2
                }
            }
        }

        Component {
            id: dominoComponent
            RectangleBoxBody {
                width: 10
                height: 50
                x: 0
                y: 510
                color: "black"

                world: physicsWorld
                bodyType: Body.Dynamic

                density: 0.001
                friction: 0.3
                restitution: 0.5
            }
        }

        function createDominos() {
            var i;

            for (i = 0; i < 5; i++) {
                var newDomino = dominoComponent.createObject(root);
                newDomino.x = 500 + 50 * i + 150;
                newDomino.y = 510;
            }
            for (i = 0; i < 4; i ++) {
                newDomino = dominoComponent.createObject(root);
                newDomino.x = 525 + 50 * i + 150;
                newDomino.y = 480;
                newDomino.rotation = 90;
            }
            for (i = 0; i < 4; i ++) {
                newDomino = dominoComponent.createObject(root);
                newDomino.x = 525 + 50 * i + 150;
                newDomino.y = 450;
            }
            for (i = 0; i < 3; i ++) {
                newDomino = dominoComponent.createObject(root);
                newDomino.x = 550 + 50 * i + 150;
                newDomino.y = 420;
                newDomino.rotation = 90;
            }
        }

//        function createChain() {
//            var i, prev = chainAnchor;
//            for (i = 0; i < 12; i++) {
//                var y = 300 + i * 20 - 5;
//                var newLink = link.createObject(root, {y: y});
//                var newJoint = linkJoint.createObject(root, {bodyA: prev.body, bodyB: newLink.body});
//                prev = newLink;
//            }
//        }

        Component.onCompleted: {
            createDominos();
//            createChain();
        }

        RectangleBoxBody {
            id: ground
            world: physicsWorld
            height: 40
            anchors {
                left: parent.left
                right: parent.right
                bottom: parent.bottom
            }
            friction: 1
            density: 0.001
            color: "#DEDEDE"
        }

        Wall {
            id: topWall
            height: 40
            anchors {
                left: parent.left
                right: parent.right
                top: parent.top
            }
        }

        Wall {
            id: leftWall
            width: 40
            anchors {
                left: parent.left
                top: parent.top
                bottom: parent.bottom
                bottomMargin: 40
            }
        }

        Wall {
            id: rightWall
            width: 40
            anchors {
                right: parent.right
                top: parent.top
                bottom: parent.bottom
                bottomMargin: 40
            }
        }

        ImageBoxBody {
            id: canon
            bodyType: Body.Dynamic
            world: physicsWorld
            x: 150
            y: 443
            collidesWith: 0
            source: "../assets/cannon.png"
            density: 0.0005
        }

        ImageBoxBody {
            id: canonBase
            x: 50
            y: 493
            sensor: true
            bodyType: Body.Static
            source: "../assets/cannon_base.png"
            world: physicsWorld
            density: 0.0005
        }

        RevoluteJoint {
            id: joint
            bodyA: canonBase.body
            bodyB: canon.body
            localAnchorA: Qt.point(75, 18)
            localAnchorB: Qt.point(36, 19)
            collideConnected: false
            motorSpeed: 0
            enableMotor: false
            maxMotorTorque: 10000
            enableLimit: true
            lowerAngle: 0
            upperAngle: -60
        }

//        RectangleBoxBody {
//            id: chainAnchor
//            width: 20
//            height: 20
//            x: 400
//            y: 230
//            color: "black"
//            world: physicsWorld
//        }

        Rectangle {
            id: debugButton
            x: 50
            y: 50
            width: 120
            height: 30
            Text {
                id: debugButtonText
                text: "Debug view: off"
                anchors.centerIn: parent
            }
            color: "#DEDEDE"
            border.color: "#999"
            radius: 5
            MouseArea {
                anchors.fill: parent
                onClicked: {
                    debugDraw.visible = !debugDraw.visible;
                    debugButtonText.text = debugDraw.visible ? "Debug view: on" : "Debug view: off";
                }
            }
        }

        Rectangle {
            id: upButton
            x: 50
            y: 90
            width: 50
            height: 50
            Text {
                id: upButtonText
                text: "up"
                anchors.centerIn: parent
            }
            color: "#DEDEDE"
            border.color: "#999"
            radius: 5
            MouseArea {
                acceptedButtons: Qt.LeftButton
                anchors.fill: parent
                onPressed: {
                    canon.fixture.density = 0.5;
                    joint.motorSpeed = -15;
                    joint.enableMotor = true;
                    upButton.color = "#AAA";
                    gearSound.play();
                }
                onReleased: {
                    joint.motorSpeed = 0;
                    upButton.color = "#DEDEDE";
                    gearSound.stop()
                }
            }
        }

        Rectangle {
            id: downButton
            x: 110
            y: 90
            width: 50
            height: 50
            Text {
                id: downButtonText
                text: "down"
                anchors.centerIn: parent
            }
            color: "#DEDEDE"
            border.color: "#999"
            radius: 5
            MouseArea {
                acceptedButtons: Qt.LeftButton
                anchors.fill: parent
                onPressed: {
                    joint.motorSpeed = 15;
                    joint.enableMotor = true;
                    downButton.color = "#AAA";
                    gearSound.play();
                }
                onReleased: {
                    joint.motorSpeed = 0;
                    downButton.color = "#DEDEDE";
                    gearSound.stop();
                }
            }
        }

        Rectangle {
            id: shotButton
            x: 170
            y: 90
            width: 50
            height: 50

            Text {
                id: shotButtonText
                text: "shot!"
                anchors.centerIn: parent
            }
            color: "#DEDEDE"
            border.color: "#999"
            radius: 5
            MouseArea {
                acceptedButtons: Qt.LeftButton
                anchors.fill: parent
                onClicked: {
                    var angle = Math.abs(joint.getJointAngle());
                    var offsetX = 65 * Math.cos(angle * Math.PI / 180);
                    var offsetY = 65 * Math.sin(angle * Math.PI / 180);
                    var newBall = ball.createObject(root);
                    newBall.x = 125 + offsetX;
                    newBall.y = 500 - offsetY;
                    var impulse = power.value;
                    var impulseX = impulse * Math.cos(angle * Math.PI / 180);
                    var impulseY = impulse * Math.sin(angle * Math.PI / 180);
                    newBall.body.applyLinearImpulse(Qt.point(impulseX, -impulseY), newBall.body.getWorldCenter());
                    shotSound.play();
                }
            }
        }

        Slider {
            id: power
            minimumValue: 0.01 * 32
            maximumValue: 5 * 32
            value: 3 * 32
            width: 200
            height: 30
            x: 230
            y: 100
        }

        DebugDraw {
            id: debugDraw
            world: physicsWorld
            visible: false
            z: 1
        }

        SoundEffect { id: shotSound; source: "../assets/cannon.wav" }
        SoundEffect { id: gearSound; source: "../assets/gear.wav" }
    }
}
