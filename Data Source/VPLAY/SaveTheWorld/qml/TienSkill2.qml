import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "TienSkill2"
    id:idSkillTien2

    SpriteSequenceVPlay {
        id: idtienSkill2
        SpriteVPlay {
            id:s1
            name: "tienSkill1"
            frameCount: 3
            frameRate: 6
            frameWidth: 69
            frameHeight: 69
            reverse: true
            source: "../assets/skills/tienSkill2.png"
            to:{"tienSkill1end" :1}
        }

        SpriteVPlay {
            id:s2
            name: "tienSkill1end"
            frameCount: 1
            frameRate: 6
            frameWidth: 69
            frameHeight: 69
            startFrameColumn: 1
            reverse: true
            source: "../assets/skills/tienSkill2.png"
        }
    }

    property point destination
    property point enemyDirect
    property int moveDuration

    PropertyAnimation on x {
        from: enemyDirect.x - 35
        to: destination.x //cho thoat khoi man hinh
        duration: moveDuration
    }

    PropertyAnimation on y {
        from: enemyDirect.y
        to: destination.y //cho thoat khoi man hinh
        duration: moveDuration
    }

    BoxCollider {
        anchors.fill: idtienSkill2
        collisionTestingOnlyMode: true
        fixture.onBeginContact: {
            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer") {
                collidedEntity.removeEntity() //xoa dan
                removeEntity() //xoa enemy
            }
            if(collidedEntity.entityType === "skillAuraBlast" || collidedEntity.entityType === "skillKamehameha") {
                removeEntity() //xoa enemy
            }
        }
    }

    Timer{
        running:true
        repeat: true
        onTriggered: {
            if(idSkillTien2.x <= rtgGamePlay.x)
                removeEntity()
        }
    }
}// EntityBase
