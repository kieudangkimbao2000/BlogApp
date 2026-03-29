import { useEffect, useState} from "react";
import type { CategoryDTO } from "../models/category-dto";
import '../assets/css/select-categ-component.css'
import { useOutletContext } from "react-router-dom";
import type { SearchBlogReqDTO } from "../models/search-blog-req-dto";
import CategoryService from "../services/categ-service";
import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
import BlogAppMessage from "../common/message";

const service = new CategoryService();

const SelectCategComponent = () => {
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage(); 
    const [categs, setCategs] = useState<CategoryDTO[]>([]);
    var {searchRef, isSearching} = useOutletContext() as {searchRef: React.RefObject<SearchBlogReqDTO>, isSearching: boolean};
    const [selectCategs, setSelectCategs] = useState<string[]>([...searchRef.current.categories]);

    useEffect(() => {
        handleGetCategs();
    },[]);

    const handleGetCategs = async () => {
        showLoading();
        try{
            const resp = await service.getCategs();
            setCategs(resp.datas);
            hideLoading();
        }catch(err)
        {
            hideLoading();
            await showMessage({
                type: BlogAppMessage.MSG_ERR_TYPE,
                message: 'Lỗi hệ thống. Hãy thử loading lại trang.'
            });
        }
    };

    const handleSelectCateg = (value: string) => {
        let selCategs = selectCategs ? [...selectCategs] : [''] as string[];
        if(selCategs.includes(value, 0))
        {
            selCategs = selCategs.filter(x => x !== value);
        }
        else
        {
            selCategs.push(value);
        }

        setSelectCategs([...selCategs]);
    };

    useEffect(() => {
        if(!searchRef?.current) return;
        searchRef.current = {...searchRef.current, categories: [...selectCategs]}
    },[selectCategs]);
                                
    return (
        <>
            <MessageComponent />
            <LoadingComponent />
            <div className="categ-area">
                <ul className="categ-list">
                    {
                        categs.map((categ) =>
                            <li className={'categ-item ' +
                                    (selectCategs.includes(categ.name, 0) ? 'select-categ-item' : '')} 
                                onClick={() => handleSelectCateg(categ.name)}>{categ.name}</li>
                        )
                    }
                </ul>
            </div>
        </>
    );
};

export default SelectCategComponent;