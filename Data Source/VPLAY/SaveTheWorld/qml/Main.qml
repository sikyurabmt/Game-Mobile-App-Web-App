import VPlay 2.0
import QtQuick 2.0
import ".."

GameWindow {
    id: gameWindow

    activeScene: sceneMenu
    width: 960
    height: 640
    visible: true
    property var __Stage: 0

    Scene {
        id: sceneMenu
        visible: true

        Rectangle {
            id: idMenu
            anchors.fill: sceneMenu

            Image {
                anchors.fill: idMenu
                source: "../assets/backgrounds/menu.jpg"
                SequentialAnimation on opacity {
                    PropertyAnimation {
                        from: 0
                        to: 1
                        duration: 5000
                    }
                }
            }

            Column {
                spacing: 2
                anchors.horizontalCenter: parent.horizontalCenter
                y: 130
                height: buttonStart.height

                ImageButton {
                    id: buttonStart
                    source: "../assets/button/button_start.png"
                    onClicked: {
                        sceneMenu.visible = false
                        gameWindow.activeScene = sceneStage
                        sceneStage.visible = true
                    }
                }

                ImageButton {
                    source: "../assets/button/button_help.png"
                    onClicked: {
                        sceneMenu.visible = false
                        gameWindow.activeScene = sceneHelps
                        sceneHelps.visible = true
                    }
                }

                ImageButton {
                    source: "../assets/button/button_credits.png"
                    onClicked: {
                        sceneMenu.visible = false
                        gameWindow.activeScene = sceneCredits
                        sceneCredits.visible = true
                    }
                }

                ImageButton {
                    source: "../assets/button/button_exit.png"
                    onClicked: {
                        gameWindow.close()
                    }
                }
            }
        }
    }

    Scene {
        id: sceneHelps
        visible: false

        Rectangle {
            id: idHelps
            anchors.fill: sceneHelps

            Image {
                anchors.fill: idHelps
                source: "../assets/backgrounds/helps.png"
                SequentialAnimation on opacity {
                    PropertyAnimation {
                        from: 0
                        to: 1
                        duration: 5000
                    }
                }
            }

            Item {
                x: 10
                y: 280

                Image {
                    id: btnBack_Helps
                    width: 65
                    height: 30
                    source: "../assets/button/button_back.png"
                }

                MouseArea {
                    anchors.fill: btnBack_Helps
                    onClicked: {
                        sceneHelps.visible = false
                        gameWindow.activeScene = sceneMenu
                        sceneMenu.visible = true
                    }
                    onPressed: {
                        btnBack_Helps.opacity = 0.5
                    }

                    onReleased: {
                        btnBack_Helps.opacity = 1.0
                    }
                }
            }
        }
    }

    Scene {
        id: sceneCredits
        visible: false

        Rectangle {
            id: idCredits
            anchors.fill: sceneCredits

            Image {
                anchors.fill: idCredits
                source: "../assets/backgrounds/credits.png"
                SequentialAnimation on opacity {
                    PropertyAnimation {
                        from: 0
                        to: 1
                        duration: 5000
                    }
                }
            }

            Item {
                x: 10
                y: 280

                Image{
                    id: btnBack_Credits
                    width: 65
                    height: 30
                    source: "../assets/button/button_back.png"
                }

                MouseArea {
                    anchors.fill: btnBack_Credits
                    onClicked: {
                        sceneCredits.visible = false
                        gameWindow.activeScene = sceneMenu
                        sceneMenu.visible = true
                    }
                    onPressed: {
                        btnBack_Credits.opacity = 0.5
                    }

                    onReleased: {
                        btnBack_Credits.opacity = 1.0
                    }
                }
            }
        }
    }

    Scene {
        id: sceneStage
        visible: false

        Rectangle {
            id: idStage
            anchors.fill: sceneStage

            Image {
                anchors.fill: idStage
                source: "../assets/backgrounds/select_stage.png"
                SequentialAnimation on opacity {
                    PropertyAnimation {
                        from: 0
                        to: 1
                        duration: 5000
                    }
                }
            }

            Item {
                Image{
                    id: imgEarth
                    x: 40
                    y: 60
                    width: 170
                    height: 100
                    source: "../assets/backgrounds/earth.png"
                }
                MouseArea {
                    anchors.fill: imgEarth
                    onClicked: {
                        sceneStage.visible = false
                        gameWindow.activeScene = sceneEarth
                        sceneEarth.visible = true
                        __Stage = 0
                        timer1.running = true
                        player.resetInfo()
                    }
                    onPressed: {
                        imgEarth.opacity = 0.5
                    }

                    onReleased: {
                        imgEarth.opacity = 1.0
                    }
                }
            }

            Item {
                Image{
                    id: imgNamek
                    x: 40
                    y: 170
                    width: 170
                    height: 100
                    source: "../assets/backgrounds/namek.png"
                }
                MouseArea {
                    anchors.fill: imgNamek
                    onClicked: {
                        sceneStage.visible = false
                        gameWindow.activeScene = sceneEarth
                        sceneEarth.visible = true
                        __Stage = 1
                        timer1.running = true
                        player.resetInfo()
                    }
                    onPressed: {
                        imgNamek.opacity = 0.5
                    }

                    onReleased: {
                        imgNamek.opacity = 1.0
                    }
                }
            }

            Item {
                x: 10
                y: 280

                Image{
                    id: btnBack_Stage
                    width: 65
                    height: 30
                    source: "../assets/button/button_back.png"
                }

                MouseArea {
                    anchors.fill: btnBack_Stage
                    onClicked: {
                        sceneStage.visible = false
                        gameWindow.activeScene = sceneMenu
                        sceneMenu.visible = true
                    }
                    onPressed: {
                        btnBack_Stage.opacity = 0.5
                    }

                    onReleased: {
                        btnBack_Stage.opacity = 1.0
                    }
                }
            }
        }
    }

    EntityManager {
        id: entityManager
        entityContainer: sceneEarth
    }

    Scene {
        id: sceneEarth
        visible: false
        width: 480
        height: 320
        Keys.forwardTo: player.controller
        property real secondTime: 0

        Rectangle {
            id: rtgGamePlay
            x: 70
            y: 0
            width: 410
            height: 260

            Image {
                anchors.fill: rtgGamePlay
                source: "../assets/backgrounds/stage"+__Stage+".png"
            }

            //            ParallaxScrollingBackground {
            //                anchors.fill: rtgGamePlay
            //                movementVelocity: Qt.point(-30,0) // van toc parallax
            //                ratio: Qt.point(1.5,1.0)
            //                sourceImage: "../assets/backgrounds/parallax_earth.png"
            //            }

        }

        MouseArea {
            id : mousePress
            anchors.fill: rtgGamePlay
            onReleased: {
                player.spiritblastSkill()
            }
        }

        Rectangle {
            id: rtgMenu
            x: 0
            y: 0
            width: 70
            height: 320

            Image {
                id: imgGP
                anchors.fill: rtgMenu
                opacity: 0.5
                source: "../assets/icons/dragonz.png"
                //                SequentialAnimation on opacity {
                //                    loops: Animation.Infinite
                //                    PropertyAnimation {
                //                        to: 0.8
                //                        duration: 300
                //                    }
                //                    PropertyAnimation {
                //                        to: 0.9
                //                        duration: 500
                //                    }
                //                }
            }

            Item {
                x: 2.5
                y: 5

                Image{
                    id: btnBack_Earth
                    width: 65
                    height: 30
                    opacity: 0.8
                    source: "../assets/button/button_back.png"
                }

                MouseArea {
                    anchors.fill: btnBack_Earth
                    onClicked: {
                        sceneEarth.visible = false
                        gameWindow.activeScene = sceneStage
                        sceneStage.visible = true

                        timer1.running = false
                        timer1.restart()
                        timer1.stop()
                    }

                    onPressed: {
                        btnBack_Earth.opacity = 0.5
                    }

                    onReleased: {
                        btnBack_Earth.opacity = 0.8
                    }
                }
            }

            Item {
                id: iconbuttonUPDown
                x: 15
                y: 230
                width: 40
                height: 85

                Image {
                    id: btUP
                    width: 40
                    height: 40
                    opacity: 0.7
                    source: "../assets/directs/direct_up.png"
                }

                Image {
                    id: btDown
                    y: 45
                    width: 40
                    height: 40
                    opacity: 0.7
                    source: "../assets/directs/direct_down.png"
                }

                MouseArea {
                    anchors.fill: iconbuttonUPDown
                    onPressed: {
                        if (mouseY < 40){
                            player.controller.yAxis = 1
                        }
                        if (mouseY > 45){
                            player.controller.yAxis = -1
                        }
                    }
                    onPositionChanged: {
                        if (mouseY < 45){
                            player.controller.yAxis = 1
                        }
                        if (mouseY > 45){
                            player.controller.yAxis = -1
                        }
                    }

                    onReleased: {
                        player.controller.yAxis = 0
                        player.standAnimation()
                    }
                }
            }
        }

        Rectangle {
            id: rtgInfo
            x: 70
            y: 260
            width: 410
            height: 60

            Image {
                id: imgIP
                anchors.fill: rtgInfo
                source: "../assets/icons/bar.png"
            }

            Image {
                id: image_player
                x: 16
                y: 3.5
                width: 81
                height: 39.5
                source: "../assets/players/player_image.png"
            }

            PlayerHPMP {
                id: playerHPMP
            }

            Item {
                id: iconskillAuraBlast
                x: 244
                y: 2
                width: 40
                height: 44

                SpriteSequenceVPlay {
                    id: spSB
                    width:40
                    height: 44
                    SpriteVPlay {
                        id: spSB1
                        name: "aurablast"
                        frameCount: 1
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/icon_aurablast.png"
                    }
                    SpriteVPlay {
                        id: spSB2
                        name: "number"
                        frameCount: 3
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/time_3.png"
                        to: {"aurablast":1}
                    }
                }

                MouseArea {
                    anchors.fill: iconskillAuraBlast
                    onPressed: {
                        if (player.__MP>0) {
                            spSB.jumpTo("number")
                            player.aurablastSkill()
                            playerHPMP.downMP()
                        }
                    }
                }
            }

            Item {
                id: iconskillKamehameha
                x: iconskillAuraBlast.x + 40
                y: iconskillAuraBlast.y
                width: iconskillAuraBlast.width
                height: iconskillAuraBlast.height

                SpriteSequenceVPlay {
                    id: spKM
                    width: 40
                    height: 44
                    SpriteVPlay {
                        id: spKM1
                        name: "kamehameha"
                        frameCount: 1
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/icon_kamehameha.png"
                    }
                    SpriteVPlay {
                        id: spKM2
                        name: "number"
                        frameCount: 9
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/time_9.png"
                        to: {"kamehameha":1}
                    }
                }

                MouseArea {
                    anchors.fill: iconskillKamehameha
                    onPressed: {
                        if (player.__MP>0) {
                            spKM.jumpTo("number")
                            player.kamehamehaSkill()
                            playerHPMP.downMP()
                        }
                    }
                }
            }

            Item {
                id: iconskillShield
                x: iconskillKamehameha.x + 40
                y: iconskillAuraBlast.y
                width: iconskillAuraBlast.width
                height: iconskillAuraBlast.height

                SpriteSequenceVPlay {
                    id: spS
                    width: 40
                    height: 44
                    SpriteVPlay {
                        id: spS1
                        name: "shield"
                        frameCount: 1
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/icon_ki.png"
                    }
                    SpriteVPlay {
                        id: spS2
                        name: "number"
                        frameCount: 7
                        frameWidth: 80
                        frameHeight: 80
                        frameRate: 2
                        source: "../assets/icons/time_7.png"
                        to: {"shield":1}
                    }
                }

                MouseArea {
                    anchors.fill: iconskillShield
                    onPressed: {
                        if (player.__MP>0 && player.__MP<4) {
                            spS.jumpTo("number")
                            player.shieldSkill()
                            playerHPMP.upMP()
                        }
                    }
                }
            }

            Item {
                id: iconLock
                x: iconskillShield.x + 40
                y: iconskillAuraBlast.y
                width: iconskillAuraBlast.width
                height: iconskillAuraBlast.height

                Image {
                    id: spL
                    width: 40
                    height: 44
                    source: "../assets/icons/icon_lock.png"
                }
            }
        }

        Player {
            id: player
        }

        Component {
            id: skillPlayer1

            SkillSpiritBlast {
                id: skill1
            }
        }

        Component {
            id: skillPlayer2

            SkillAuraBlast {
                id: skill2
            }
        }

        Component {
            id: skillPlayer3

            SkillKamehameha {
                id: skill3
            }
        }

        Component {
            id: senzuBeans

            SenzuBeans {
                id: senzu
            }
        }

        Component {
            id: saibama

            EnemySaibama {
                id: idsaibama
            }
        }

        Component{
            id: bardockOozaru

            BardockOozaru{
                id: oozaru
            }
        }

        Component{
            id: android18
            Android18{
                id: idandroid
            }
        }

        Component{
            id: buu

            Buu{
                id: idbuu
                y: idbuu.randNums*50
            }
        }

        Component{
            id: skillBuu
            SkillBuu{
                id: skill
            }
        }

        Component{
            id: skillBardockOozaru
            SkillBardockOozaru{
                id: skill1
            }
        }
        Component{
            id: skillAndroid18
            SkillAndroid18{
                id: skill
            }
        }

        Component{
            id: tien
            Tien{
                id:idTien
            }
        }
        Component{
            id:tienSkill
            SkillTien{
                id:skilltien
            }
        }
        Component{
            id:tienSkill2
            TienSkill2{
                id:skilltien2
            }
        }

        Component{
            id:tienSkill3
            TienSkill3{
                id:skilltien3
            }
        }

        Component{
            id: cell
            Cell{
                id: idcell
            }
        }

        Component{
            id:cellSkill1
            CellSkill1{
                id:idcellSkill1
            }
        }
        Component{
            id:cellSkill2
            CellSkill2{
                id:idcellSkill2
            }
        }

        Timer {
            id: timer1
            running: false
            repeat: true
            interval: 1000
            onTriggered:{
                sceneEarth.secondTime++
                if(__Stage  === 0){
                    if(sceneEarth.secondTime < 20  ){
                        entityManager.createEntityFromComponent(saibama)
                    }

                    if(19 < sceneEarth.secondTime && sceneEarth.secondTime < 40){
                        if((sceneEarth.secondTime) %5 === 0){
                            entityManager.createEntityFromComponent(android18)
                        }
                    }

                    if(40 < sceneEarth.secondTime && sceneEarth.secondTime < 70 && sceneEarth.secondTime %3 === 0){
                        entityManager.createEntityFromComponent(bardockOozaru)
                    }

                    if(sceneEarth.secondTime === 71){
                        timer1.repeat = false
                        entityManager.createEntityFromComponent(cell)
                        sceneEarth.secondTime = 80
                    }

                    if(sceneEarth.secondTime === 80){

                        timer1.repeat = true
                    }
                }
                if(__Stage === 1){
                    if(sceneEarth.secondTime < 20  ){
                        entityManager.createEntityFromComponent(saibama)
                    }

                    if(19 < sceneEarth.secondTime && sceneEarth.secondTime < 40){
                        if((sceneEarth.secondTime) %5 === 0){
                            entityManager.createEntityFromComponent(android18)
                        }
                    }

                    if(40 < sceneEarth.secondTime && sceneEarth.secondTime < 70 && sceneEarth.secondTime %3 === 0){
                        entityManager.createEntityFromComponent(buu)
                    }

                    if(sceneEarth.secondTime === 71){
                        timer1.repeat = false
                        entityManager.createEntityFromComponent(tien)
                        sceneEarth.secondTime = 80
                    }

                    if(sceneEarth.secondTime === 80){

                        timer1.repeat = true
                    }
                }
            }
        }

        PhysicsWorld {
            id: physicsWorld
            debugDrawVisible: true
        }
    }

    Scene {
        id: sceneLose
        visible: false

        Rectangle {
            id: idLose
            anchors.fill: sceneLose

            Image {
                anchors.fill: idLose
                source: "../assets/backgrounds/lose.png"
                SequentialAnimation on opacity {
                    PropertyAnimation {
                        from: 0
                        to: 1
                        duration: 5000
                    }
                }
            }

            Image {
                x: sceneLose.width/2-120/2
                y: sceneLose.height/2-30/2
                source: "../assets/text/text_lose.png"
                SequentialAnimation on opacity {
                    loops: Animation.Infinite
                    PropertyAnimation {
                        to: 0.7
                        duration: 500
                    }
                    PropertyAnimation {
                        to: 1
                        duration: 500
                    }
                }
            }

            Item {
                x: 10
                y: 280

                Image{
                    id: btnBack_Lose
                    width: 65
                    height: 30
                    source: "../assets/button/button_back.png"
                }

                MouseArea {
                    anchors.fill: btnBack_Lose
                    onClicked: {
                        sceneLose.visible = false
                        gameWindow.activeScene = sceneStage
                        sceneStage.visible = true
                    }
                    onPressed: {
                        btnBack_Lose.opacity = 0.5
                    }

                    onReleased: {
                        btnBack_Lose.opacity = 1.0
                    }
                }
            }
        }
    }

    Scene {
        id: sceneWin
        visible: false

        Rectangle {
            id: idWin
            anchors.fill: sceneWin

            Image {
                anchors.fill: idWin
                source: "../assets/backgrounds/win.png"
                SequentialAnimation on opacity {
                    PropertyAnimation {
                        from: 0
                        to: 1
                        duration: 5000
                    }
                }
            }

            Image {
                x: sceneWin.width/2-120/2
                y: sceneWin.height/2-30/2
                source: "../assets/text/text_win.png"
                SequentialAnimation on opacity {
                    loops: Animation.Infinite
                    PropertyAnimation {
                        to: 0.7
                        duration: 500
                    }
                    PropertyAnimation {
                        to: 1
                        duration: 500
                    }
                }
            }

            Item {
                x: 10
                y: 280

                Image{
                    id: btnBack_Win
                    width: 65
                    height: 30
                    source: "../assets/button/button_back.png"
                }

                MouseArea {
                    anchors.fill: btnBack_Win
                    onClicked: {
                        sceneWin.visible = false
                        gameWindow.activeScene = sceneStage
                        sceneStage.visible = true
                    }
                    onPressed: {
                        btnBack_Win.opacity = 0.5
                    }

                    onReleased: {
                        btnBack_Win.opacity = 1.0
                    }
                }
            }
        }
    }
}
