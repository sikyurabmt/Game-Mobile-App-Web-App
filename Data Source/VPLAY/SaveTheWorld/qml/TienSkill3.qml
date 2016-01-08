import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "TienSkill3"
    id:idSkillTien3

    SpriteSequenceVPlay {
        id: idtienSkill3
        SpriteVPlay {
            name: "tienSkill1"
            frameCount: 2
            frameRate: 6
            frameWidth: 18
            frameHeight: 17
            reverse: true
            source: "../assets/skills/tienSkill3.png"
        }
    }

    property point destination
    property point enemyDirect
    property int moveDuration

    PropertyAnimation on x {
        from: enemyDirect.x
        to: destination.x //cho thoat khoi man hinh
        duration: moveDuration
    }

    PropertyAnimation on y {
        from: enemyDirect.y + 25
        to: destination.y //cho thoat khoi man hinh
        duration: moveDuration
    }

    BoxCollider {
        anchors.fill: idtienSkill3
        collisionTestingOnlyMode: true
        fixture.onBeginContact: {
            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "player") {
                removeEntity()
            }
        }
    }

    Timer{
        running:true
        repeat: true
        onTriggered: {
            if(idSkillTien3.x <= rtgGamePlay.x)
                removeEntity()
        }
    }
}// EntityBase
