export enum PhoneTypes {
    Home,
    Mobile,
    Other
}

export namespace PhoneTypes {
    export function values() {
        return Object.keys(PhoneTypes).filter(
            (type) => isNaN(<any>type) && type !== 'values'
        );
    }
}