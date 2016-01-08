import VPlay 2.0
import QtQuick 2.0
import QtMultimedia 5.5

EntityBase {
    entityType: "enemy"
    id: cells
    x: 400
    y: 100
    property var _EHP: 4
    property var count: 0
    property var isShoot: false
    property var iFrametoDie: 0

    SpriteSequenceVPlay {
        id: enemyCells

        //width: 72
        //height: 84

        //y: utils.generateRandomValueBetween(0, rtgGamePlay.height-enemyCells.height)


        SpriteVPlay {
            name: "appear"
            frameCount: 8
            frameRate: 4
            frameWidth: 120
            frameHeight: 57
            source: "../assets/enemies/cell/cell_appear.png"
            to: {"stand":1}
        }

        SpriteVPlay {
            name: "stand"
            frameCount: 4
            frameRate: 8
            frameWidth: 40
            frameHeight: 84
            source: "../assets/enemies/cell/cell_stand.png"
            //to: {"disappear":1}
        }

        SpriteVPlay {
            name: "disappear"
            frameCount: 8
            frameRate: 8
            frameWidth: 120
            frameHeight: 57
            source: "../assets/enemies/cell/cell_disappear.png"
            // to: {"appear":1}
        }

        SpriteVPlay {
            name: "absorb"
            frameCount: 4
            frameRate: 10
            frameWidth: 72
            frameHeight: 103
            source: "../assets/enemies/cell/cell_absorb.png"
            to: {"stand":1}
        }

        SpriteVPlay {
            name: "shoot"
            frameCount: 7
            frameRate: 10
            frameWidth: 72
            frameHeight: 86
            source: "../assets/enemies/cell/cell_shoot.png"
            //to: {"stand":1}
        }

        SpriteVPlay {
            name: "die"
            frameCount: 2
            frameRate: 4
            frameWidth: 80
            frameHeight: 61
            source: "../assets/enemies/cell/cell_die.png"
            to: {"died":1}
        }

        SpriteVPlay {
            name: "died"
            frameCount: 1
            startFrameColumn: 3
            frameRate: 4
            frameWidth: 80
            frameHeight: 61
            source: "../assets/enemies/cell/cell_die.png"
            to: {"explore" :1}
        }

        SpriteVPlay {
            name: "explore"
            frameWidth: 70
            frameHeight: 54
            frameCount: 4
            frameRate: 8
            source:"../assets/enemies/bacdockOozaruDie.png"
        }

    }

    BoxCollider {
        anchors.fill: enemyCells
        collisionTestingOnlyMode: true // use Box2D only for collision detection, move the entity with the NumberAnimation above
        fixture.onBeginContact: {

            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer") {
                collidedEntity.removeEntity() //xoa dan
                if(_EHP === 1 || _EHP === 2 || _EHP === 3) {
                    _EHP = _EHP + 1
                    absorbAnimation()
                }
            }
            if (collidedEntity.entityType === "skillAuraBlast" || collidedEntity.entityType === "skillKamehameha") {
                collidedEntity.removeEntity()
                if(_EHP > 0) {
                    _EHP =_EHP - 1
                }
                if(_EHP === 0) {
                    dieAnimation()
                }
            }
        }
    }

    MovementAnimation {
        target: parent
        property: "pos"
        velocity: Qt.point(-50, 0)
        running: true
        id: animation
    }

    Timer{
        id: tmCell
        running: true
        repeat: true
        interval: 1000
        onTriggered: {
            if(count === 0){
                var random = utils.generateRandomValueBetween(0,4)
                cells.y = 20 * random
                animation.velocity = Qt.point(-50,0)
                appearAnimation()
                if(cells.x < 350 && isShoot === false){
                    animation.velocity = Qt.point(0,0)
                    shootAnimation()
                }
            }

            if(isShoot === true)
                count ++

            if( count < 5 && isShoot === true) {
                kickSkill1()
            }
            if(count > 4 && count < 10) {
                kickSkill2()
            }
            if(count > 9){
                count ++
                animation.velocity = Qt.point(30, 0)
                disappearAnimation()
                if(cells.x > 450)
                    animation.velocity = Qt.point(0,0)
                if(count === 15)
                    count = 0
            }
            if(_EHP === 0){
                iFrametoDie ++;
                if(iFrametoDie === 2) {
                    removeEntity()
                    tmCell.stop()
                    sceneEarth.visible = false
                    gameWindow.activeScene = sceneWin
                    sceneWin.visible = true
                }
            }

            if(player.__isDie === 1) {
                removeEntity()
                tmCell.stop()
                sceneEarth.visible = false
                gameWindow.activeScene = sceneLose
                sceneLose.visible = true
            }
        }
    }

    function appearAnimation() {
        enemyCells.jumpTo("appear")
        isShoot = false
    }

    function standAnimation() {
        enemyCells.jumpTo("stand")
        isShoot = true
    }

    function disappearAnimation() {
        enemyCells.jumpTo("disappear")
        isShoot = false
    }

    function absorbAnimation() {
        enemyCells.jumpTo("absorb")
        isShoot = false
        cellGotit.play()
    }

    function dieAnimation() {
        enemyCells.jumpTo("die")
        isShoot = false
        cellDie.play()
        player.__isWin = 1
    }

    function shootAnimation() {
        enemyCells.jumpTo("shoot")
        isShoot = true
    }

    function kickSkill1(){
        var destination = Qt.point(player.x, player.y)
        entityManager.createEntityFromComponentWithProperties(cellSkill1, {"appear": Qt.point(cells.x,cells.y),"destination": destination})
        cellAttack1.play()
    }
    function kickSkill2(){
        var destination = Qt.point(player.x, player.y)
        entityManager.createEntityFromComponentWithProperties(cellSkill2, {"appear": Qt.point(cells.x,cells.y),"destination": destination})
        cellAttack2.play()
    }
    MediaPlayer { id: cellAttack1; source: "../assets/sounds/cell/attack1.wav" }
    MediaPlayer { id: cellAttack2; source: "../assets/sounds/cell/attack2.wav" }
    MediaPlayer { id: cellDie; source: "../assets/sounds/cell/die.wav" }
    MediaPlayer { id: cellGotit; source: "../assets/sounds/cell/gotit.wav" }
}
