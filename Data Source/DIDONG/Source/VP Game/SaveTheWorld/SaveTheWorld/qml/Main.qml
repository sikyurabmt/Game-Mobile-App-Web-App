import VPlay 2.0
import QtQuick 2.0

GameWindow {
    id: gameWindow

    activeScene: scene
    width: 960
    height: 640
    visible: true

    EntityManager {
        id: entityManager
        entityContainer: scene
    }

    Scene {
        id: scene

        width: 480
        height: 320

        Rectangle {
            id: rtgGamePlay
            x: 70
            y: 0
            width: 410
            height: 260

            ParallaxScrollingBackground {
                anchors.fill: rtgGamePlay
                movementVelocity: Qt.point(-30,0) // van toc parallax
                ratio: Qt.point(1.5,1.0)
                sourceImage: "../assets/backgrounds/earth.png"
            }

        }

        MouseArea {
            id : mousePress
            anchors.fill: rtgGamePlay
            onReleased: {
                player.spiritblastSkill()
            }
        }

        Rectangle {
            id: rtgMenu
            x: 0
            y: 0
            width: 70
            height: 320

            Image {
                id: imgGP
                anchors.fill: rtgMenu
                opacity: 0.5
                source: "../assets/icons/dragonz.png"
            }

            Item {
                id: iconbuttonUPDown
                x: 15
                y: 230
                width: 40
                height: 85

                Image {
                    id: btUP
                    width: 40
                    height: 40
                    opacity: 0.7
                    source: "../assets/directs/direct_up.png"
                }

                Image {
                    id: btDown
                    y: 45
                    width: 40
                    height: 40
                    opacity: 0.7
                    source: "../assets/directs/direct_down.png"
                }

                MouseArea {
                    anchors.fill: iconbuttonUPDown
                    onPressed: {
                        if (mouseY < 40){
                            player.controller.yAxis = 1
                        }
                        if (mouseY > 45){
                            player.controller.yAxis = -1
                        }
                    }
                    onPositionChanged: {
                        if (mouseY < 45){
                            player.controller.yAxis = 1
                        }
                        if (mouseY > 45){
                            player.controller.yAxis = -1
                        }
                    }

                    onReleased: {
                        player.controller.yAxis = 0
                        player.standAnimation()
                    }
                }
            }
        }

        Rectangle {
            id: rtgInfo
            x: 70
            y: 260
            width: 410
            height: 60

            Image {
                id: imgIP
                anchors.fill: rtgInfo
                source: "../assets/icons/bar.png"
            }

            Image {
                id: image_player
                x: 16
                y: 3.5
                width: 81
                height: 39.5
                source: "../assets/players/player_image.png"
            }

            PlayerHPMP {
                id: playerHPMP
            }

            Item {
                id: iconskillAuraBlast
                x: 244
                y: 2
                width: 40
                height: 44

                SpriteSequenceVPlay {
                    id: spSB
                    width:40
                    height: 44
                    SpriteVPlay {
                        id: spSB1
                        name: "aurablast"
                        frameCount: 1
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/icon_aurablast.png"
                    }
                    SpriteVPlay {
                        id: spSB2
                        name: "number"
                        frameCount: 3
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/time_3.png"
                        to: {"aurablast":1}
                    }
                }

                MouseArea {
                    anchors.fill: iconskillAuraBlast
                    onPressed: {
                        spSB.jumpTo("number")
                        player.aurablastSkill()
                        playerHPMP.downMP()
                    }
                }
            }

            Item {
                id: iconskillKamehameha
                x: iconskillAuraBlast.x + 40
                y: iconskillAuraBlast.y
                width: iconskillAuraBlast.width
                height: iconskillAuraBlast.height

                SpriteSequenceVPlay {
                    id: spKM
                    width: 40
                    height: 44
                    SpriteVPlay {
                        id: spKM1
                        name: "kamehameha"
                        frameCount: 1
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/icon_kamehameha.png"
                    }
                    SpriteVPlay {
                        id: spKM2
                        name: "number"
                        frameCount: 9
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/time_9.png"
                        to: {"kamehameha":1}
                    }
                }

                MouseArea {
                    anchors.fill: iconskillKamehameha
                    onPressed: {
                        spKM.jumpTo("number")
                        player.kamehamehaSkill()
                        playerHPMP.downMP()
                    }
                }
            }

            Item {
                id: iconskillShield
                x: iconskillKamehameha.x + 40
                y: iconskillAuraBlast.y
                width: iconskillAuraBlast.width
                height: iconskillAuraBlast.height

                SpriteSequenceVPlay {
                    id: spS
                    width: 40
                    height: 44
                    SpriteVPlay {
                        id: spS1
                        name: "shield"
                        frameCount: 1
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/icon_ki.png"
                    }
                    SpriteVPlay {
                        id: spS2
                        name: "number"
                        frameCount: 7
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/time_7.png"
                        to: {"shield":1}
                    }
                }

                MouseArea {
                    anchors.fill: iconskillShield
                    onPressed: {
                        spS.jumpTo("number")
                        player.shieldSkill()
                        playerHPMP.upHP()//Nham nhi Vc :v
                        playerHPMP.upMP()
                    }
                }
            }

            Item {
                id: iconLock
                x: iconskillShield.x + 40
                y: iconskillAuraBlast.y
                width: iconskillAuraBlast.width
                height: iconskillAuraBlast.height

                Image {
                    id: spL
                    width: 40
                    height: 44
                    source: "../assets/icons/icon_lock.png"
                }
            }
        }

        Keys.forwardTo: player.controller

        Player {
            id: player
        }

        Component {
            id: skillPlayer1

            SkillSpiritBlast {
                id: skill1
            }
        }

        Component {
            id: skillPlayer2

            SkillAuraBlast {
                id: skill2
            }
        }

        Component {
            id: skillPlayer3

            SkillKamehameha {
                id: skill3
            }
        }

        Component {
            id: monsterEnemy

            Monster {
                id: monster
            }
        }

        Timer {
            running: true
            repeat: true
            interval: 2000
            onTriggered:{
                entityManager.createEntityFromComponent(monsterEnemy)
            }
        }

        PhysicsWorld {
            id: physicsWorld
            debugDrawVisible: false
        }
    }
}
