export interface RequestBaseDTO<T> {
    datas: T;
    base64Strings?: string[];

    [key: string]: any;
}