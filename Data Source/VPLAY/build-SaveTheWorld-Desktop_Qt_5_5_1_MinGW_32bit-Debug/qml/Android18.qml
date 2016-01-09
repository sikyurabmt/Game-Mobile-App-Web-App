import QtQuick 2.0
import VPlay 2.0
import QtMultimedia 5.5

EntityBase {
    id: enemies
    entityType: "Android18"

    property var directX: enemies.x - enemies.width
    property var directY: enemies.y + enemies.height/4
    property var count: 1
    property var time: 1
    property var flag: false
    x: rtgGamePlay.width - enemies.width
    y: utils.generateRandomValueBetween(0,rtgGamePlay.height-enemies.height - 60,50)

    SpriteSequenceVPlay{
        id: sAndroid18
        SpriteVPlay{
            name: "kick"
            frameWidth: 60
            frameHeight: 81
            frameCount: 5
            frameRate: 5
            source:"../assets/enemies/androidKick.png"
        }
        SpriteVPlay{
            name: "die"
            frameWidth: 70
            frameHeight: 54
            frameCount: 4
            frameRate: 8
            source:"../assets/enemies/bacdockOozaruDie.png"

        }
    }


    function kickSkill(){
        var moveDuration = 500
        var x = enemies.x - player.x
        var y = enemies.y - player.y
        entityManager.createEntityFromComponentWithProperties(skillAndroid18, {"destination": Qt.point(rtgGamePlay.x,player.y+player.height/2), "moveDuration": moveDuration, "enemyDirect": Qt.point(enemies.x ,enemies.y - enemies.height*2/3)})
        androidAttack.play()
    }

    Timer {
        id: time
        running: true
        repeat: true
        interval: 1000
        onTriggered:{
            if(flag === false)
            kickSkill()
           if(flag === true)
               removeEntity()
        }
    }

    function android18Kick(){
        sAndroid18.jumpTo("kick")
    }

    function android18Die(){
        flag = true
        sAndroid18.jumpTo("die")
    }

    BoxCollider {
        anchors.fill: sAndroid18
        collisionTestingOnlyMode: true // use Box2D only for collision detection, move the entity with the NumberAnimation above
        fixture.onBeginContact: {

            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer") {
                android18Die()
            }
            if(collidedEntity.entityType === "skillAuraBlast" || collidedEntity.entityType === "skillKamehameha") {
                android18Die()
            }
            if(collidedEntity.entityType === "player") {
               android18Die()
            }
        }
    }
    MediaPlayer { id: androidAttack; source: "../assets/sounds/android18/attack.wav" }
}
