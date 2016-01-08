import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "TienSkill"
    id:idSkillTien

    SpriteSequenceVPlay {
        id: idtienSkill
        SpriteVPlay {
            name: "tienSkill1"
            frameCount: 4
            frameRate: 6
            frameWidth: 70
            frameHeight: 66
            reverse: true
            source: "../assets/skills/tienSkill1.png"
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
        from: enemyDirect.y
        to: destination.y //cho thoat khoi man hinh
        duration: moveDuration
    }

    BoxCollider {
        anchors.fill: idtienSkill
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
            if(idSkillTien.x <= rtgGamePlay.x)
                removeEntity()
        }
    }
}// EntityBase
