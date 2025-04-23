export class FileFormat {
    public static FileLineDelimiters: Array<FileFormat> = [
        { id: 0, extension: '.txt', description: 'Text File' },
        { id: 1, extension: '.csv', description: 'Comma Separated Files' },
    ]

    public id: number | undefined;
    public extension: string | undefined;
    public description: string | undefined;
}

export class FileLineDelimiter {
    public static FileLineDelimiters: Array<FileLineDelimiter> = [
        { lineDelimiter: '\r\n', description: 'Carriage Return + Line Feed(Windows) ' },
        { lineDelimiter: '\r', description: 'Carriage Return (Unix) ' },
        { lineDelimiter: '\n', description: 'Line Feed(MacOS) ' }
    ]

    public lineDelimiter: string | undefined;
    public description: string | undefined;
}

export class FileColumnDelimiter {
    public static FileColumnDelimiters: Array<FileColumnDelimiter> = [
        { columnDelimiter: ',', description: 'Comma , ' },
        { columnDelimiter: ';', description: 'Semi-colon ;' },
        { columnDelimiter: '\t', description: 'Tab' }
    ]

    public columnDelimiter: string | undefined;
    public description: string | undefined;
}

export class FileDecimalSymbol {
    public static FileDecimalSymbols: Array<FileDecimalSymbol> = [
        { decimalSymbol: '.', description: 'Point ' },
        { decimalSymbol: ',', description: 'Comma' },
    ]

    public decimalSymbol: string | undefined;
    public description: string | undefined;
}

export class FileThousandSeparator {
    public static FileThousandSeparators: Array<FileThousandSeparator> = [
        { format: '', description: 'None\t ["1000000"]' },
        { format: ' ', description: 'Space\t ["1 000 000"]' },
        { format: ',', description: 'Comma\t ["1,000,000"]' },
        { format: '.', description: 'Point\t ["1.000.000"]' },
    ]

    public format: string | undefined;
    public description: string | undefined;
}

