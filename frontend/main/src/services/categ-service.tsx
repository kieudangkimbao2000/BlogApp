import ApiClient from "../api/apiclient";
import type { CategoryListRespDTO } from "../models/category-list-resp-dto";

class CategoryService{

    async getTop5() : Promise<CategoryListRespDTO>
    {
        const resp = await ApiClient.get<CategoryListRespDTO>('categ/topfive', null);

        return resp;
    }
}

export default CategoryService;