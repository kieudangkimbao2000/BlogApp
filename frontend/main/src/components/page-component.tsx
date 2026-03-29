import { useEffect } from 'react';
import '../assets/css/page-component.css'

interface PageComponentProps
{
    curPage: number;
    totalPages: number;
    handleSearch: (page: number) => void;
}

const PageComponent = (props: PageComponentProps) => {
    useEffect(() => {
        const pageElm  = document.getElementById('page-' + props.curPage);

        if(!pageElm) return;

        pageElm.classList.add('page-enabled');
    },[]);

    return (
        <>
            <div className="pagination">
                <div>
                    <a onClick={() => props.curPage > 1 ? props.handleSearch(1) : null} >{'<<'}</a>
                </div>
                <div onClick={() => props.curPage > 1 ? props.handleSearch(props.curPage - 1) : null}>
                    <a>{'<'}</a>
                </div>
                {(props.totalPages >= 23 && props.curPage >= 20) ? <div>...</div> : <></>}
                <div>
                    {(props.totalPages < 23) ? 
                        [...Array(props.totalPages)].map((_, index) => (
                            <a id={'page-' + (index + 1)} className='page ' onClick={()=>props.handleSearch(index + 1)}>
                                {index + 1}
                            </a>
                        )) : 
                        [...Array(20)].map((_, index) => (
                            (props.curPage < props.totalPages -3) ?
                            <a id={'page-' +  ((props.curPage >= 20) ? ((index + 1) + (props.curPage + 2) - 20) : index + 1)}
                                    onClick={()=>props.handleSearch((props.curPage >= 20) ? ((index + 1) + (props.curPage + 2) - 20) : index + 1)}
                                    className='page'>
                                {(props.curPage >= 20) ? ((index + 1) + (props.curPage + 2) - 20) : index + 1}
                            </a> 
                                :
                            <a onClick={()=>props.handleSearch(props.totalPages + (index + 1 - 20))}>{props.totalPages + (index + 1 - 20)}</a>    
                        ))
                    }   
                </div>
                {(props.totalPages >= 23 && props.curPage < props.totalPages - 3) ?
                    <>
                        <div>...</div>
                        <div>
                            {[...Array(3)].map((_, index) => (
                                <a onClick={()=>props.handleSearch(props.totalPages - 2 + index)}>{(props.totalPages - 2 + index)}</a>
                            ))
                            }
                        </div>
                    </> : <></>
                }
                <div><a onClick={() => props.curPage < props.totalPages ? props.handleSearch(props.curPage + 1) : null}>{'>'}</a></div>
                <div><a onClick={() => props.curPage < props.totalPages ? props.handleSearch(props.totalPages) : null}>{'>>'}</a></div>
            </div>
        </>
    );
};

export default PageComponent;