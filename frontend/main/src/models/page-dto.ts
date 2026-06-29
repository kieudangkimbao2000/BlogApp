export interface PageDTO<T>{
    datas?: T[];
    curPage?: number;
    pageSize: number;
}