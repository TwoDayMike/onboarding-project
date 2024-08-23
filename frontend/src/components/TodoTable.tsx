import React, { useMemo, useState } from 'react';
import useTodos from "hooks/useTodos";
import { Suspense } from "react";
import { TodoExampleDTO } from "services/backend/client.generated";
import usePutTodo from 'hooks/usePutTodo';
import useDeleteTodo from 'hooks/useDeleteTodo';

function TodoTable() {
    const { data, isError, isLoading } = useTodos();
    const { mutateAsync } = usePutTodo();
    const {mutateAsync: deleteTodo} = useDeleteTodo();
    const [filter, setFilter] = useState(false);

    if (isLoading || isError) {
        return <div>Loading...</div>; // Placeholder loading state
    }

    function clearAllCompletedTask() {
        const completedTodos = data.filter(x => x.isCompleted).map(x => x.id);
        const requests = completedTodos.map(x => deleteTodo(x))
        Promise.all(requests).then(() => {
            console.log("Finished")
        });
    }

    function renderContent(filter: boolean) {
        if (!filter) {
            return (
                data.map((todo: TodoExampleDTO) => (
                    <tr key={todo.id}>
                        <td>{todo.id}</td>
                        <td>{todo.name}</td>
                        <td>{todo.typeName}</td>
                        <td>{todo.description}</td>
                        <td><input type="checkbox" onChange={(e) => mutateAsync({ id: todo.id, isCompleted: e.target.checked })} checked={todo.isCompleted} /></td>
                        <td>
                            {/* Update Button */}
                            <button onClick={() => console.log(`Update ${todo.id}`)}>Update</button>
                            {/* Delete Button */}
                            <button onClick={() => console.log(todo.id)}>Delete</button>
                        </td>
                    </tr>
                ))
            )
        }
        return (
            data.filter(x => x.isCompleted === false).map((todo: TodoExampleDTO) => (
                <tr key={todo.id}>
                    <td>{todo.id}</td>
                    <td>{todo.name}</td>
                    <td>{todo.typeName}</td>
                    <td>{todo.description}</td>
                    <td><input type="checkbox" onChange={(e) => mutateAsync({ id: todo.id, isCompleted: e.target.checked })} checked={todo.isCompleted} /></td>
                    <td>
                        {/* Update Button */}
                        <button onClick={() => console.log(`Update ${todo.id}`)}>Update</button>
                        {/* Delete Button */}
                        <button onClick={() => console.log(todo.id)}>Delete</button>
                    </td>
                </tr>
            ))
        )

    }
    return (
        <>
            <button onClick={() => setFilter(!filter)}>{filter ? "Get all todos" : "Filter completed todos"}</button>
            <button onClick={() => clearAllCompletedTask()}>DESTROY EVERY COMPLETED TASK!</button>
            <table style={{ width: '100%', borderCollapse: 'collapse' }}>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Type Name</th>
                        <th>Description</th>
                        <th>Completed</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {renderContent(filter)}
                </tbody>
            </table>
        </>
    );
}

export default TodoTable;
