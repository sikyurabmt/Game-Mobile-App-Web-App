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

        property int velocityX: -30 //van toc parallax

        ParallaxScrollingBackground {
            anchors.centerIn: parent
            movementVelocity: Qt.point(root.velocityX,0)
            ratio: Qt.point(0.5,1.0)
            sourceImage: "../assets/background-mountains.png"
        }
        ParallaxScrollingBackground {
            anchors.centerIn: parent
            movementVelocity: Qt.point(root.velocityX,0)
            ratio: Qt.point(1.0,1.0)
            sourceImage: "../assets/background-hills.png"
            sourceImage2: "../assets/background-hills2.png"
        }

        ParallaxScrollingBackground {
            anchors.centerIn: parent
            movementVelocity: Qt.point(root.velocityX,0)
            ratio: Qt.point(1.3,1.0)
            sourceImage: "../assets/background-lawn.png"
        }

        SpriteSequenceVPlay {
            id: spriteSequence

            x: 50
            y: 400

            SpriteVPlay {
                name: "tank"
                frameCount: 2
                frameRate: 7

                frameWidth: 114
                frameHeight: 90
                source: "../assets/tank.png"
            }
        }

        World { id: physicsWorld }
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

        //bottomWall
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

//        Wall {
//            id: topWall
//            height: 40
//            anchors {
//                left: parent.left
//                right: parent.right
//                top: parent.top
//            }
//        }

//        Wall {
//            id: leftWall
//            width: 40
//            anchors {
//                left: parent.left
//                top: parent.top
//                bottom: parent.bottom
//                bottomMargin: 40
//            }
//        }

//        Wall {
//            id: rightWall
//            width: 40
//            anchors {
//                right: parent.right
//                top: parent.top
//                bottom: parent.bottom
//                bottomMargin: 40
//            }
//        }

        MouseArea {
            acceptedButtons: Qt.LeftButton
            anchors.fill: parent
            onClicked: {
                //3 diem P1, P2, P3 => angle la goc tai P1 hop boi P2 va P3
                var P1 = Qt.point(50+80,400+20);
                var P2 = Qt.point(mouseX-5,mouseY-5);//tru` body ball cho dung ti le
                var P3 = Qt.point(P1.x+100,P1.y);
                var angle = Math.atan2(P2.y - P1.y, P2.x - P1.x) - Math.atan2(P3.y - P1.y, P3.x - P1.x);
                var offsetX = P1.x;
                var offsetY = P1.y;
                var newBall = ball.createObject(root);
                newBall.x = offsetX;
                newBall.y = offsetY;
                var impulse = 2.8 * 32; //van toc ban
                var impulseX = impulse * Math.cos(angle);
                var impulseY = impulse * Math.sin(angle);
                newBall.body.applyLinearImpulse(Qt.point(impulseX, impulseY), newBall.body.getWorldCenter());
                shotSound.play();
            }
        }

        DebugDraw {
            id: debugDraw
            world: physicsWorld
            visible: false
        }

        SoundEffect { id: shotSound; source: "../assets/cannon.wav" }
    }
}
