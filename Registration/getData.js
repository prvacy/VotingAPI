import parse from 'csv-parse';
import fs from 'fs';
import {finished} from 'stream/promises';

export const getData = async () => {
    let list = [];
    let voters = [];
    const parser = fs.createReadStream('./list.csv').pipe(
        parse({ columns: false, delimiter: ';' })
    );
    parser.on('readable', function () {
        let record;
        while (record = parser.read()) {
            record = record.map(str => str.replace(/\s/g, '')); //delete whitespaces
            let voter = {
                id: record[0],
                group: record[1],
                surname: record[2],
                name: record[3],
                mName: record[4],
                mail: record[5]
            }
            voters.push(voter);
        }
    });



    await finished(parser);
    return voters;
}

//getData();