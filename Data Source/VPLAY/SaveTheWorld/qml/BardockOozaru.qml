import QtQuick 2.0
import VPlay 2.0
import QtMultimedia 5.5

EntityBase {
    id: enemies
    entityType: "BardockOozaru"

    property var directX: enemies.x - enemies.width
    property var directY: enemies.y + enemies.height/4
    property var countSkill :5
    property var count: 1
    property var time: 1
    property var flag: false
    property var flagtoKick: 2
    x: rtgGamePlay.width
    y: utils.generateRandomValueBetween(0, rtgGamePlay.height-enemies.height - 60) // 60: khoang cach giua scene va rtgGamePlay

    SpriteSequenceVPlay{
        id: sBardockOozaru
        SpriteVPlay{
            name: "start"
            frameWidth: 75
            frameHeight: 54
            frameCount: 4
            frameRate: 8
            source:"../assets/enemies/bardockOozaruRun.png"
        }
        SpriteVPlay{
            name: "kick"
            frameWidth: 76
            frameHeight: 54
            frameCount: 3
            frameRate: 3
            source:"../assets/enemies/bardockOozaruKick.png"
        }
        SpriteVPlay{
            name: "die"
            frameWidth: 70
            frameHeight: 54
            frameCount: 4
            frameRate: 8
            to: {"die1":1 }
            source:"../assets/enemies/bacdockOozaruDie.png"

        }
        SpriteVPlay{
            name: "die1"
            frameWidth: 70
            frameHeight: 54
            frameCount: 1
            startFrameColumn: 4
            frameRate: 8
            source:"../assets/enemies/bacdockOozaruDie.png"

        }
        SpriteVPlay{
            name: "fly"
            frameWidth: 107
            frameHeight: 54
            frameCount: 3
            frameRate: 1
            source:"../assets/enemies/bardockOozaruFly.png"
        }
    }

    function kickSkill(){
        var moveDuration = 500
        var x = enemies.x - player.x
        var y = enemies.y - player.y
        entityManager.createEntityFromComponentWithProperties(skillBardockOozaru, {"destination": Qt.point(rtgGamePlay.x,player.y+player.height/2), "moveDuration": moveDuration, "enemyDirect": Qt.point(enemies.x ,enemies.y - enemies.height*2/3)})
        bardockAttack.play()
    }

    Timer {
        id: time
        running: true
        repeat: true
        interval: 1000
        onTriggered:{
            if( enemies.x < 350 && countSkill > 0 && flag === false){

                animation.running = false;
                enemies.bardockOozaruKick();
                enemies.kickSkill();
                countSkill = countSkill - 1;
            }
            if(countSkill === 0 && flag === false){
                enemies.bardockOozaruFly();
                animation.running = true;
                animation.velocity = Qt.point(-(enemies.x - player.x),-(enemies.y-player.y))
            }

            if(flag === true)
            {
                removeEntity()
            }
        }
    }
    function bardockOozaruStart(){
        sBardockOozaru.jumpTo("start")
    }

    function bardockOozaruKick(){
        sBardockOozaru.jumpTo("kick")
    }

    function bardockOozaruDie(){
        sBardockOozaru.jumpTo("die")
        animation.running = false
        time.interval = 500
        flag = true
    }

    function bardockOozaruFly(){
        sBardockOozaru.jumpTo("fly");
    }

    MovementAnimation {
        target: parent
        property: "pos"
        velocity: Qt.point(-20, 0)
        running: true
        id: animation
    }
    BoxCollider {
        anchors.fill: sBardockOozaru
        collisionTestingOnlyMode: true // use Box2D only for collision detection, move the entity with the NumberAnimation above
        fixture.onBeginContact: {

            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer") {
                bardockOozaruDie()
            }
            if(collidedEntity.entityType === "skillAuraBlast" || collidedEntity.entityType === "skillKamehameha") {
                bardockOozaruDie()
            }
            if(collidedEntity.entityType === "player") {
                bardockOozaruDie()
            }
        }
    }

    MediaPlayer { id: bardockAttack; source: "../assets/sounds/bardock/attack.wav" }
}
