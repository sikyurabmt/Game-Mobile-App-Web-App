import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "SkillBuu"
    id: idSkillBuu
    MultiResolutionImage {
        id: skillImage
        source: "../assets/skills/buuSkill.png"
    }

    // these values can then be set when a new projectile is created in the MouseArea below
    property point destination
    property point enemyDirect
    property int moveDuration

    PropertyAnimation on x {
        from: enemyDirect.x - skillImage.width + 20 //  10 doan boi trang giua 2 sprite
        to: destination.x //cho thoat khoi man hinh
        duration: moveDuration
    }

    PropertyAnimation on y {
        from: enemyDirect.y + skillImage.height*1.8
        to: enemyDirect.y + skillImage.height*1.8 //cho thoat khoi man hinh
        duration: moveDuration
    }

    BoxCollider {
        anchors.fill: skillImage
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
            if(idSkillBuu.x <= rtgGamePlay.x)
                removeEntity()
        }
    }
}// EntityBase
