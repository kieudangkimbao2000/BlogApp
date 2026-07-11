import { useEffect, useState} from "react";
import type { TagDTO, SearchBlogReqDTO } from "../models/generated-interfaces";
import '../assets/css/select-categ-component.css'
import { useOutletContext } from "react-router-dom";
import CategoryService from "../services/tag-service";
import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
import BlogAppMessage from "../common/message";

const service = new CategoryService();

const SelectTagComponent = () => {
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage(); 
    const [tags, setTags] = useState<TagDTO[]>([]);
    var {searchRef, isSearching} = useOutletContext() as {searchRef: React.RefObject<SearchBlogReqDTO>, isSearching: boolean};
    const [selectTags, setSelectTags] = useState<string[]>([...searchRef.current.tags ?? []]);

    useEffect(() => {
        handleGetCategs();
    },[]);

    const handleGetCategs = async () => {
        showLoading();
        try{
            const resp = await service.getCategs();

            if(!resp || !resp.datas || resp.statusCode != 200)
            {
                hideLoading();
                return;
            }

            setTags(resp.datas as TagDTO[]);
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
        let selCategs = selectTags ? [...selectTags] : [''] as string[];
        if(selCategs.includes(value, 0))
        {
            selCategs = selCategs.filter(x => x !== value);
        }
        else
        {
            selCategs.push(value);
        }

        setSelectTags([...selCategs]);
    };

    useEffect(() => {
        if(!searchRef?.current) return;
        searchRef.current = {...searchRef.current, tags: [...selectTags]}
    },[selectTags]);
                                
    return (
        <>
            <MessageComponent />
            <LoadingComponent />
            <div className="categ-area">
                <ul className="categ-list">
                    {
                        tags.map((tag) =>
                            <li className={'categ-item ' +
                                    (selectTags.includes(tag.name ?? '', 0) ? 'select-categ-item' : '')} 
                                onClick={() => handleSelectCateg(tag.name ?? '')}>{tag.name}</li>
                        )
                    }
                </ul>
            </div>
        </>
    );
};

export default SelectTagComponent;