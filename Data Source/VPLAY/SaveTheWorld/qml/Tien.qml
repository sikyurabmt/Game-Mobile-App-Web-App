import QtQuick 2.0
import VPlay 2.0
import QtMultimedia 5.5

EntityBase {
    entityType: "Tien"
    id: enemies
    y: 100
    x :400
    property var kind: 1
    property var count: 0
    property  var isKick: true
    property var _EHP: 4
    property var isDie: false
    property var iFrameToExplore: 0

    SpriteSequenceVPlay {
        id: sTien

        SpriteVPlay {
            name: "hoisinh"
            frameCount: 2
            frameRate: 6
            frameWidth: 63
            frameHeight: 126
            source: "../assets/enemies/tienHoiSinh.png"
        }
        SpriteVPlay {
            name: "chuong3"
            frameCount: 3
            frameRate: 3
            frameWidth: 106
            frameHeight: 126
            reverse: true
            source: "../assets/enemies/tienChuong3.png"
        }

        SpriteVPlay {
            name: "chuong"
            frameCount: 9
            frameRate: 9
            frameWidth: 125
            frameHeight: 126
            source: "../assets/enemies/tienChuong.png"
        }
        SpriteVPlay {
            name: "chuong2"
            frameCount: 6
            frameRate: 6
            frameWidth: 93
            frameHeight: 126
            reverse: true
            source: "../assets/enemies/tienChuong2.png"
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


    function chuongTien(){
        isKick = true
        sTien.jumpTo("chuong")
        kind = 1
    }

    function chuong2Tien(){
        isKick = true
        sTien.jumpTo("chuong2")
        kind = 2
    }

    function chuong3Tien(){
        isKick = true
        sTien.jumpTo("chuong3")
        kind = 3
        enemies.x = enemies.x -30
    }

    function hoisinhTien(){
        sTien.jumpTo("hoisinh")
        isKick = false
    }

    function dieTien(){
        sTien.jumpTo("explore")
        isKick = false
        isDie = true
        tienDie.play()
    }

    function kickSkill(){
        var moveDuration = 1000
        var x =  player.x
        var y =  player.y + player.height/2
        var xE = enemies.x - enemies.width
        var yE = enemies.y - enemies.height
        entityManager.createEntityFromComponentWithProperties(tienSkill, {"destination": Qt.point(x,y), "moveDuration": moveDuration, "enemyDirect": Qt.point(xE,yE)})
        tienAttack3.play()
    }

    function kickSkill2(){
        var moveDuration = 1000
        var x =  player.x
        var y =  player.y + player.height/2
        var xE = enemies.x - enemies.width
        var yE = enemies.y - enemies.height
        entityManager.createEntityFromComponentWithProperties(tienSkill2, {"destination": Qt.point(x,y), "moveDuration": moveDuration, "enemyDirect": Qt.point(xE, yE)})
        tienAttack2.play()
    }

    function kickSkill3(){
        var moveDuration = 1000
        var x =  player.x
        var y =  player.y + player.height/2
        var xE = enemies.x - enemies.width
        var yE = enemies.y - enemies.height
        entityManager.createEntityFromComponentWithProperties(tienSkill3, {"destination": Qt.point(x,y), "moveDuration": moveDuration, "enemyDirect": Qt.point(xE, yE)})
        tienAttack1.play()
    }

    BoxCollider {
        anchors.fill: sTien
        collisionTestingOnlyMode: true // use Box2D only for collision detection, move the entity with the NumberAnimation above
        fixture.onBeginContact: {

            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer") {
                collidedEntity.removeEntity() //xoa dan
                _EHP =_EHP - 1
            }
            if (collidedEntity.entityType === "skillAuraBlast") {
                collidedEntity.removeEntity()
                _EHP =_EHP - 1

            }
            if(collidedEntity.entityType === "skillKamehameha") {
                if(_EHP>1) {
                    _EHP=_EHP-1
                }
            }
            if(_EHP === 0)
                dieTien()
        }
    }

    Timer{
        id: time
        running:true
        repeat:true
        interval: 1000
        onTriggered: {
            count++
            if(isDie === false){
                var random = utils.generateRandomValueBetween(0,4)
                enemies.y = 20 * random
            }

            if(count == 1)
                chuong3Tien()
            if(count === 9 )
                chuong2Tien()
            if(count  > 20){

                chuongTien()
            }

            if(count === 25)
                count = 0
            if(isKick === true){
                if(kind === 1)
                    kickSkill()
                if(kind === 2)
                    kickSkill2()
                if(kind === 3)
                    kickSkill3()
            }
            if(isDie === true){
                iFrameToExplore++
                if(iFrameToExplore === 2)
                    removeEntity()
            }
            if(player.__isDie === 1) {
                removeEntity()
                time.stop()
                sceneEarth.visible = false
                gameWindow.activeScene = sceneLose
                sceneLose.visible = true
            }
        }
    }
    MediaPlayer { id: tienAttack1; source: "../assets/sounds/tien/attack1.wav" }
    MediaPlayer { id: tienAttack2; source: "../assets/sounds/tien/attack2.wav" }
    MediaPlayer { id: tienAttack3; source: "../assets/sounds/tien/attack3.wav" }
    MediaPlayer { id: tienDie; source: "../assets/sounds/tien/die.wav" }
}
