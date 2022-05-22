import { Builder, By, Key, until } from 'selenium-webdriver';
import chromedriver from 'chromedriver';

import { getData } from './getData.js';
import config from './config.js';

export const app = async () => {
    let driver = new Builder().forBrowser('chrome').build();

    //Go to main page
    let url = config.url;
    await driver.get(url);

    //Click on login link
    let loginLink = await driver.findElement(By.css('a[href="In_regist.html"]'));
    await loginLink.click();

    //Click ok on alert
    await driver.wait(until.alertIsPresent());
    await driver.switchTo().alert().accept();

    let table = await driver.findElement(By.css('tr'));
    //let stationSelect = new Select(driver.findElement(By.css('select'))[0]);
    let selectItem = await driver.findElement(By.xpath(`//*[@id="answer"]/option[text()="${config.voteStation}"]`));
    await selectItem.click();

    let submit = await driver.findElement(By.id('submit'));
    await submit.click();


    let voters = await getData();
    for (const voter of voters) {
        //Send password
        let login = config.login;
        let pass = config.password;
        let loginItem = await driver.findElement(By.xpath(`//*[@id="REGI"]/option[text()="${login}"]`));
        await loginItem.click();

        let passInput = await driver.findElement(By.id('PWREG'));
        passInput.sendKeys(pass);

        let confirmButton = await driver.findElement(By.xpath('//input[@value="Пароль введено"]'));
        await confirmButton.click();
        //Click ok on alert
        await driver.wait(until.alertIsPresent());
        await driver.switchTo().alert().accept();

        //await new Promise(resolve => setTimeout(resolve, config.timeout));

        await driver.wait(until.alertIsPresent());
        let alert = await driver.switchTo().alert();
        await alert.sendKeys(voter.id);
        await alert.accept();

        //wait for confirmation
        await driver.wait(until.alertIsPresent());
        await driver.switchTo().alert().accept();

        //Fill data
        let email = await driver.findElement(By.id('EMAIL'));
        email.clear();
        email.sendKeys(voter.mail);

        let sName = await driver.findElement(By.id('FAMIL'));
        sName.clear();
        sName.sendKeys(voter.surname);

        let name = await driver.findElement(By.id('IMJA'));
        name.clear();
        name.sendKeys(voter.name);

        var mName = await driver.findElement(By.id('OTCH'));
        mName.clear();
        mName.sendKeys(voter.mName);

        let year = await driver.findElement(By.id('RIK'));
        year.clear();
        year.sendKeys('2000')

        let day = await driver.findElement(By.id('CHISLO'));
        day.clear();
        day.sendKeys('01')

        let sendDataButton = await driver.findElement(By.xpath('//input[@value="Відправити дані до реєстру"]'));
        sendDataButton.click();

        await driver.wait(until.alertIsPresent());//Wait until data is written to register
        alert = await driver.switchTo().alert();
        console.log(alert.getText());
        alert.accept();

        await new Promise(res => setTimeout(res, config.timeout));//wait 3 sec

        await driver.navigate().back();
        await driver.navigate().forward();
    }

}