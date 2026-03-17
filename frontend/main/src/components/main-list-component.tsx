const MainListComponent = () => {
    
    return (
        <div className='container list-area'>
            {[...Array(10)].map((_, index) => (
                <div className='row item'>
                    <div className='col item-img' style={{width: '100%', height: '100%'}}>
                        <img  style={{width: '100%', height: '100%'}} src='https://thumbs.dreamstime.com/b/blog-woodn-dice-depicting-letters-stack-newspapers-leaning-dice-34801080.jpg' />
                    </div>
                    <div className='col-8 item-title'>
                        Blog Title
                    </div>
                    <div className='col-2 item-date'>
                        username
                        <br/> 
                        25/01/02 15:33
                    </div>
                </div>
            ))}
        </div>
    );

};

export default MainListComponent;