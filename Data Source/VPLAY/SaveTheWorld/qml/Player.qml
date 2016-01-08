import VPlay 2.0
import QtQuick 2.0
import QtMultimedia 5.5

EntityBase {
    id: player
    entityType: "player"
    x: 82
    y: 100
    width: 56
    height: 57

    property var sceneTop: 50
    property var sceneBottom: 155
    property var directX: player.x + player.width
    property var directY: player.y + player.height*0.23
    property var __HP: 4
    property var __MP: 4
    property var __isDie : false
    property var __isWin : false

    signal upPressed(variant event)
    signal downPressed(variant event)

    property alias controller: twoAxisController

    property real moveValue: -250

    SpriteSequenceVPlay {
        id: ssPlayer


        SpriteVPlay {
            name: "stand"
            frameCount: 3
            frameRate: 6
            frameWidth: 56
            frameHeight: 57
            source: "../assets/players/player_stand.png"
            //visible: false
        }

        SpriteVPlay {
            name: "flyUp"
            frameCount: 1
            frameRate: 10
            frameWidth: 59
            frameHeight: 54
            source: "../assets/players/player_flyup.png"
            //visible: false
        }

        SpriteVPlay {
            name: "flyDown"
            frameCount: 1
            frameRate: 10
            frameWidth: 47
            frameHeight: 76
            source: "../assets/players/player_flydown.png"
            //visible: false
        }

        SpriteVPlay {
            name: "shield"
            frameCount: 3
            frameRate: 5
            frameWidth: 64
            frameHeight: 67
            source: "../assets/players/player_shield.png"
            to: {"stand":1}
        }

        SpriteVPlay {
            name: "spiritblast"
            frameCount: 4
            frameRate: 12
            frameWidth: 56
            frameHeight: 60
            source: "../assets/players/player_spiritblast.png"
            to: {"stand":1}
        }

        SpriteVPlay {
            name: "aurablast"
            frameCount: 6
            frameRate: 12
            frameWidth: 56
            frameHeight: 64
            source: "../assets/players/player_aurablast.png"
            to: {"stand":1}
        }
        SpriteVPlay {
            name: "kamehameha"
            frameCount: 4
            frameRate: 6
            frameWidth: 56
            frameHeight: 60
            source: "../assets/players/player_spiritblast.png"
            to: {"stand":1}
        }

        SpriteVPlay {
            name: "die"
            frameCount: 3
            frameRate: 6
            frameWidth: 72
            frameHeight: 63
            source: "../assets/players/player_die.png"
            to: {"died":1}
        }

        SpriteVPlay {
            name: "died"
            frameCount: 1
            startFrameColumn: 4
            frameRate: 6
            frameWidth: 72
            frameHeight: 63
            source: "../assets/players/player_die.png"
            //to: {"stand":1}
        }
    }



    BoxCollider {
        id: boxcolliderPlayer
        anchors.fill: player
        bodyType: Body.Dynamic
        fixedRotation: true
        // balancing settings:
        linearDamping: 5.0
        // set friction between 0 and 1
        friction: 0.6
        // restitution is bounciness - don't bounce, because then the state would be changed too often
        restitution: 0
        // this is needed, because otherwise when resetting the level (and the player position), and the body was sleeping before it wouldn't fall down immediately again, because it hasn't woken up from sleeping!
        sleepingAllowed: false
        fixture.onBeginContact: {
            var collidedEntity = other.getBody().target
            if(     collidedEntity.entityType === "SkillAndroid18" ||
                    collidedEntity.entityType === "SkillBardockOozaru" ||
                    collidedEntity.entityType === "BardockOozaru" ||
                    collidedEntity.entityType === "SkillBuu" ||
                    collidedEntity.entityType === "cellskill1" ||
                    collidedEntity.entityType === "cellskill2" ||
                    collidedEntity.entityType === "TienSkill" ||
                    collidedEntity.entityType === "TienSkill2" ||
                    collidedEntity.entityType === "TienSkill3" ||
                    collidedEntity.entityType === "saibama"
                    ) {
                collidedEntity.removeEntity()
                playerHPMP.downHP() //xoa enemy
                if(__HP>0) {
                    hurtSound.play()
                }
            }
            if(collidedEntity.entityType === "senzubeans") {
                collidedEntity.removeEntity()
                playerHPMP.upHP()
                playerHPMP.upMP()
                beansSound.play()
            }
            player.x = 82
        }
    }

    TwoAxisController {
        id: twoAxisController
    }

    Timer {
        id: updateTimer
        interval: 25
        running: true
        repeat: true
        onTriggered: {
            // this must be done every frame, because the linearVelocity gets reduced because of the damping!
            var yAxis = controller.yAxis
            if(yAxis) {
                if(player.y > sceneBottom){
                    if(yAxis===1) {
                        player.flyUpAnimation()
                        boxcolliderPlayer.body.linearVelocity.y = yAxis*moveValue
                    }
                }
                if(player.y < sceneTop){
                    if(yAxis===-1) {
                        player.flyDownAnimation()
                        boxcolliderPlayer.body.linearVelocity.y = yAxis*moveValue
                    }
                }
                if(player.y > sceneTop && player.y < sceneBottom){
                    if(yAxis===1) {
                        player.flyUpAnimation()
                        boxcolliderPlayer.body.linearVelocity.y = yAxis*moveValue
                    }
                    if(yAxis===-1) {
                        player.flyDownAnimation()
                        boxcolliderPlayer.body.linearVelocity.y = yAxis*moveValue
                    }
                }
            }
        }
    }

    function standAnimation() {
        ssPlayer.jumpTo("stand")
    }

    function flyUpAnimation() {
        ssPlayer.jumpTo("flyUp")
    }

    function flyDownAnimation() {
        ssPlayer.jumpTo("flyDown")
    }

    function shieldAnimation() {
        ssPlayer.jumpTo("shield")
    }

    function dieAnimation() {
        ssPlayer.jumpTo("die")
    }

    function spiritblastAnimation() {
        ssPlayer.jumpTo("spiritblast")
    }

    function aurablastAnimation() {
        ssPlayer.jumpTo("aurablast")
    }

    function kamehamehaAnimation() {
        ssPlayer.jumpTo("kamehameha")
    }

    function spiritblastSkill() {
        var offset = Qt.point(
                    mousePress.mouseX - player.directX + 70, //70 la screenTap.x
                    mousePress.mouseY - player.directY)
        if(offset.x <= 0) {
            standAnimation()
            return
        }
        spiritblastAnimation()
        var realX = rtgGamePlay.width
        var ratio = offset.y / offset.x
        var realY = (realX * ratio) + player.directY
        var destination = Qt.point(realX, realY)
        var offReal = Qt.point(realX - player.directX, realY - player.directY)
        var length = Math.sqrt(offReal.x*offReal.x + offReal.y*offReal.y)
        var velocity = 300 //speed of skill
        var realMoveDuration = length / velocity * 1000
        entityManager.createEntityFromComponentWithProperties(skillPlayer1, {"destination": destination, "moveDuration": realMoveDuration})
        spiritblastSound.play()
    }

    function aurablastSkill() {
        aurablastAnimation()
        var destination = Qt.point(rtgGamePlay.width, player.y)
        var realMoveDuration = 2200
        entityManager.createEntityFromComponentWithProperties(skillPlayer2, {"destination": destination, "moveDuration": realMoveDuration})
        aurablastSound.play()
    }

    function kamehamehaSkill() {
        kamehamehaAnimation()
        entityManager.createEntityFromComponentWithProperties(skillPlayer3, {"destination": Qt.point(0,0), "moveDuration": 10000})
        kamehamehaSound.play()
    }

    function shieldSkill() {
        shieldAnimation()
        shieldSound.play()
    }

    function playerDie() {
        dieSound.play()
    }

    function resetInfo() {
        player.x = 82
        player.y = 100
        standAnimation()
        __HP = 4
        __MP = 4
        playerHPMP.upHP()
        playerHPMP.upMP()
    }

    MediaPlayer { id: spiritblastSound; source: "../assets/sounds/player/spiritblast.wav" }
    MediaPlayer { id: aurablastSound; source: "../assets/sounds/player/aurablast.wav" }
    MediaPlayer { id: kamehamehaSound; source: "../assets/sounds/player/kamehameha.wav" }
    MediaPlayer { id: shieldSound; source: "../assets/sounds/player/shield.wav" }
    MediaPlayer { id: hurtSound; source: "../assets/sounds/player/hurt.wav" }
    MediaPlayer { id: dieSound; source: "../assets/sounds/player/die.wav" }
    MediaPlayer { id: beansSound; source: "../assets/sounds/player/beans.wav" }
}
