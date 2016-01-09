import QtQuick 2.0
import VPlay 2.0
import QtMultimedia 5.5

EntityBase {
    id: enemies
    entityType: "Buu"

    property var directX: enemies.x - enemies.width
    property var directY: enemies.y + enemies.height/4
    property var countSkill :14
    property var flag: false
    x: rtgGamePlay.width - rtgGamePlay.x - enemies.width
    property int randNums: utils.generateRandomValueBetween(0,4)

    SpriteSequenceVPlay{
        id: sBuu
        SpriteVPlay{
            name: "kick"
            frameWidth: 150
            frameHeight: 106
            frameCount: 5
            frameRate: 5
            source:"../assets/enemies/Buu.png"
        }
    }


    function kickSkill(){
        var moveDuration = 500
        var x = enemies.x - rtgGamePlay.width
        var y = enemies.y + enemies.height
        entityManager.createEntityFromComponentWithProperties(skillBuu, {"destination": Qt.point(x,y), "moveDuration": moveDuration, "enemyDirect": Qt.point(enemies.x ,y)})
        buuAttack.play()
    }

    Timer {
        id: time
        running: true
        repeat: true
        interval: 1000
        onTriggered:{
            kickSkill()
            countSkill--
            if(countSkill === 0)
                removeEntity()
            if(flag === true)
            {
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

    function buuKick(){
        sBardockOozaru.jumpTo("kick")
    }

    function buuDie(){
        flag = true
    }

    BoxCollider {
        anchors.fill: sBuu
        collisionTestingOnlyMode: true // use Box2D only for collision detection, move the entity with the NumberAnimation above
        fixture.onBeginContact: {

            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer") {
                buuDie()
            }
            if(collidedEntity.entityType === "skillAuraBlast" || collidedEntity.entityType === "skillKamehameha") {
                buuDie()
            }
            if(collidedEntity.entityType === "player") {
                buuDie()
            }
        }
    }

    MediaPlayer { id: buuAttack; source: "../assets/sounds/buu/attack.wav" }
}
