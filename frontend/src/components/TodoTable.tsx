import React, { useMemo, useState } from 'react';
import useTodos from "hooks/useTodos";
import { Suspense } from "react";
import { TodoExampleDTO } from "services/backend/client.generated";
import usePutTodo from 'hooks/usePutTodo';
import useDeleteTodo from 'hooks/useDeleteTodo';
import {Button} from './UI/Button';

function TodoTable() {
    const { data, isError, isLoading } = useTodos();
    const { mutateAsync } = usePutTodo();
    const { mutateAsync: deleteTodo } = useDeleteTodo();
    const [filter, setFilter] = useState(false);

    if (isLoading || isError) {
        return <div>Loading...</div>; // Placeholder loading state
    }

    function clearAllCompletedTask() {
        const completedTodos = data.filter(x => x.isCompleted).map(x => x.id);
        const requests = completedTodos.map(x => deleteTodo(x))
        Promise.all(requests);
    }

    function renderContent(filter: boolean): React.ReactNode {
        const cellStyle = 'px-4 py-2 border-b border-gray-200';
        const actionCellStyle = `${cellStyle} flex items-center`;
    
        if (!filter) {
            return (
                <table className='w-full min-w-max'>
                    <tbody>
                        {data.map((todo: TodoExampleDTO) => (
                            <tr key={todo.id}>
                                <td className={cellStyle}>{todo.id}</td>
                                <td className={cellStyle}>{todo.name}</td>
                                <td className={cellStyle}>{todo.typeName}</td>
                                <td className={cellStyle}>{todo.description}</td>
                                <td className={cellStyle}>
                                    <input
                                        type="checkbox"
                                        onChange={(e) => mutateAsync({ id: todo.id, isCompleted: e.target.checked })}
                                        checked={todo.isCompleted}
                                    />
                                </td>
                                <td className={actionCellStyle}>
                                    <button onClick={() => console.log(todo.id)} className='bg-blue-500 p-1 rounded-lg mr-2'>Update</button>
                                    <button onClick={() => console.log(todo.id)} className='bg-red-500 p-1 rounded-lg ml-2'>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            );
        }
        return (
            <table className='w-full min-w-max'>
                <thead>
                    <tr>
                        <th className='text-left px-6 py-3'>ID</th>
                        <th className='text-left px-6 py-3'>Name</th>
                        <th className='text-left px-6 py-3'>Type Name</th>
                        <th className='text-left px-6 py-3'>Description</th>
                        <th className='text-center px-6 py-3'>Status</th>
                        <th className='text-right px-6 py-3'>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {data.filter(x => x.isCompleted === false).map((todo: TodoExampleDTO) => (
                        <tr key={todo.id}>
                            <td className={cellStyle}>{todo.id}</td>
                            <td className={cellStyle}>{todo.name}</td>
                            <td className={cellStyle}>{todo.typeName}</td>
                            <td className={cellStyle}>{todo.description}</td>
                            <td className={cellStyle}>
                                <input
                                    type="checkbox"
                                    onChange={(e) => mutateAsync({ id: todo.id, isCompleted: e.target.checked })}
                                    checked={todo.isCompleted}
                                />
                            </td>
                            <td className={actionCellStyle}>
                                <button onClick={() => console.log(`Update ${todo.id}`)} className='bg-blue-500 p-1 rounded-lg mr-2'>Update</button>
                                <button onClick={() => deleteTodo(todo.id)} className='bg-red-500 p-1 rounded-lg ml-2'>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    }
    return (
        <>
            <div className='flex gap-2.5 items-center justify-center mb-2'>
                <Button  onClick={() => setFilter(!filter)}>{filter ? "Get all todos" : "Filter completed todos"}</Button>
                <Button  onClick={() => clearAllCompletedTask()}>DESTROY EVERY COMPLETED TASK!</Button>
            </div>
            <div className="border-t-slate-500 border-t-2 w-full flex flex-col">
                <div className='flex flex-row justify-between'>
                    <div>ID</div>
                    <div>Name</div>
                    <div>Type Name</div>
                    <div>Description</div>
                    <div>Status</div>
                    <div>Actions</div>
                </div>
                <div className='overflow-x-auto'>{renderContent(filter)}</div>
            </div>
        </>
    );
    
}

export default TodoTable;
