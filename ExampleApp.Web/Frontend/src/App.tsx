import React, { useEffect, useState } from 'react';
import './Imports.scss';
import './App.scss';

interface TodoItem {
  id: string,
  title: string,
  description: string,
  isCompleted: string
}

function getFullUrl(relativeUrl: string) {
  // this logic exists to support debugging in a browser, running on a react dev server, but connecting to the API on another port.
  let port = Number(document.location.port);

  const params =  new URLSearchParams(document.location.search);
  if (params.has("apiPort"))
    port = Number(params.get("apiPort"));

  return `http://localhost:${port}/${relativeUrl}`;
}

async function fetchItems() {
  const response = await fetch(getFullUrl("Todo"));
  return await response.json();
}

async function createNewItem(title: string, description: string) {
  const response = await fetch(getFullUrl("Todo"), {
    method: "POST",
    body: JSON.stringify({ title: title, description: description }),
    headers: [ [ "Content-Type", "Application/Json" ]]
  });
  return await response.json();
}

async function deleteItem(id: string) {
  await fetch(getFullUrl(`Todo/${id}`), {
    method: "DELETE",
  });
}

async function setItemChecked(id: string, checked: boolean) {
  await fetch(getFullUrl(`Todo/${id}`), {
    method: "PATCH",
    body: JSON.stringify(checked),
    headers: [ [ "Content-Type", "Application/Json" ]]
  });
}

function App() {
  const [items, setItems] = useState<TodoItem[]>();

  const [newItemTitle, setNewItemTitle] = useState<string>();
  const [newItemDescription, setNewItemDescription] = useState<string>();

  useEffect(() => {
    fetchItems().then(setItems)
  }, [])

  async function handleButtonClick() {
    if (!newItemTitle || !newItemDescription)
      return;

      await createNewItem(newItemTitle, newItemDescription)
      const items = await fetchItems();
      setItems(items);
  }

  async function toggleItem(item: TodoItem) {
    await setItemChecked(item.id, !item.isCompleted);
    const items = await fetchItems();
    setItems(items);
  }

  async function handleDeleteClick(item: TodoItem) {
    await deleteItem(item.id);
    const items = await fetchItems();
    setItems(items);
  }

  return (
    <div className="app">

      <div className="item-container">
        { items?.map(item => <div className="item">
          <div className="item-checker" onClick={x => toggleItem(item)}>
            { item.isCompleted ? <div className="checkmark">✓</div> : <></> }            
          </div>
          <div className="item-text">
            <span className="item-title">{ item.title }</span>
            <span className="item-description">
              { item.description }
            </span>
            <span className="item-delete">
              <div className="btn btn-outline-danger btn-sm" onClick={x => handleDeleteClick(item)}>Delete</div>
            </span>
          </div>
        </div>)}
      </div>

      <hr></hr>

      <div className="create-wrapper col-12 col-sm-9 col-md-6 col-lg-4 col-xl-3">
        <div className="create-input-wrapper">
          <label className="form-label" htmlFor="title">Title</label>
          <input className="form-control" type="text" value={newItemTitle} onChange={x => setNewItemTitle(x.target.value)}/>
        </div>
        <div className="create-input-wrapper">
          <label className="form-label" htmlFor="title">Description</label>
          <input className="form-control" type="text" value={newItemDescription} onChange={x => setNewItemDescription(x.target.value)}/>
        </div>
        <div className="button-wrapper">
          <div className="btn btn-primary" onClick={handleButtonClick}>Create</div>
        </div>
      </div>

    </div>
  );
}

export default App;
