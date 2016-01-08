import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "senzubeans"
    id: senzubeans

    Image {
        id: spsenzu
        width: 15
        height: 15
        source: "../assets/items/senzu_beans.png"
        SequentialAnimation on opacity {
            loops: Animation.Infinite
            PropertyAnimation {
                to: 0.3
                duration: 100
            }
            PropertyAnimation {
                to: 1
                duration: 100
            }
        }
    }

    y: utils.generateRandomValueBetween(0, rtgGamePlay.height-spsenzu.height)

    NumberAnimation on x {
        from: rtgGamePlay.width
        to: -200
        duration: 5000
    }

    BoxCollider {
        anchors.fill: spsenzu
    }

    Timer {
        running: true
        interval: 100
        repeat: true
        onTriggered: {
            if(senzubeans.x <= rtgGamePlay.x*1.05)
            {
                removeEntity()
            }
        }
    }
}
