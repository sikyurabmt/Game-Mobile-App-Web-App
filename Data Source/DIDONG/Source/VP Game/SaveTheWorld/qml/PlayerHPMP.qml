import VPlay 2.0
import QtQuick 2.0

EntityBase {
    id: playerHPMP
    entityType: "playerHPMP"

    SpriteSequenceVPlay {
        id: spHP
        x : 126
        y : 2.5
        width: 115
        height: 22
        SpriteVPlay {
            id: spHP1
            name: "spHP1"
            frameCount: 1
            startFrameColumn: 1
            frameWidth: 139
            frameHeight: 29
            frameRate: 1
            source: "../assets/icons/bar_hp.png"
        }
        SpriteVPlay {
            id: spHP2
            name: "spHP2"
            frameCount: 1
            startFrameColumn: 2
            frameWidth: 139
            frameHeight: 29
            frameRate: 1
            source: "../assets/icons/bar_hp.png"
        }
        SpriteVPlay {
            id: spHP3
            name: "spHP3"
            frameCount: 1
            startFrameColumn: 3
            frameWidth: 139
            frameHeight: 29
            frameRate: 1
            source: "../assets/icons/bar_hp.png"
        }
        SpriteVPlay {
            id: spHP4
            name: "spHP4"
            frameCount: 1
            startFrameColumn: 4
            frameWidth: 139
            frameHeight: 29
            frameRate: 1
            source: "../assets/icons/bar_hp.png"
        }
        SpriteVPlay {
            id: spHP5
            name: "spHP5"
            frameCount: 1
            startFrameColumn: 5
            frameWidth: 139
            frameHeight: 29
            frameRate: 1
            source: "../assets/icons/bar_hp.png"
        }
    }

    function downHP() {
        if(player.__HP===4) {
            player.__HP = 3
            spHP.jumpTo("spHP2")
            return
        }
        if(player.__HP===3) {
            player.__HP = 2
            spHP.jumpTo("spHP3")
            return
        }
        if(player.__HP===2) {
            player.__HP = 1
            spHP.jumpTo("spHP4")
            return
        }
        if(player.__HP===1) {
            player.__HP = 0
            spHP.jumpTo("spHP5")
            return
        }
    }

    function upHP() {
        player.__HP = 4
        spHP.jumpTo("spHP1")
    }

    SpriteSequenceVPlay {
        id: spMP
        x : spHP.x
        y : spHP.y + spHP.height
        width: 100
        height: 20
        SpriteVPlay {
            id: spMP1
            name: "spMP1"
            frameCount: 1
            startFrameColumn: 1
            frameWidth: 106
            frameHeight: 28
            frameRate: 1
            source: "../assets/icons/bar_mp.png"
        }
        SpriteVPlay {
            id: spMP2
            name: "spMP2"
            frameCount: 1
            startFrameColumn: 2
            frameWidth: 106
            frameHeight: 28
            frameRate: 1
            source: "../assets/icons/bar_mp.png"
        }
        SpriteVPlay {
            id: spMP3
            name: "spMP3"
            frameCount: 1
            startFrameColumn: 3
            frameWidth: 106
            frameHeight: 28
            frameRate: 1
            source: "../assets/icons/bar_mp.png"
        }
        SpriteVPlay {
            id: spMP4
            name: "spMP4"
            frameCount: 1
            startFrameColumn: 4
            frameWidth: 106
            frameHeight: 28
            frameRate: 1
            source: "../assets/icons/bar_mp.png"
        }
        SpriteVPlay {
            id: spMP5
            name: "spMP5"
            frameCount: 1
            startFrameColumn: 5
            frameWidth: 106
            frameHeight: 28
            frameRate: 1
            source: "../assets/icons/bar_mp.png"
        }
    }

    function downMP() {
        if(player.__MP===4) {
            player.__MP = 3
            spMP.jumpTo("spMP2")
            return
        }
        if(player.__MP===3) {
            player.__MP = 2
            spMP.jumpTo("spMP3")
            return
        }
        if(player.__MP===2) {
            player.__MP = 1
            spMP.jumpTo("spMP4")
            return
        }
        if(player.__MP===1) {
            player.__MP = 0
            spMP.jumpTo("spMP5")
            return
        }
    }

    function upMP() {
        player.__MP = 4
        spMP.jumpTo("spMP1")
    }
}
