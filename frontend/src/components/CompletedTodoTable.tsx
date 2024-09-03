import useTodos from "hooks/useTodos";

function CompletedTodoTable() {
    const { data, isLoading } = useTodos();

    if (isLoading)
        return <>Loading ...</>

    return (
        <table className="table-auto w-full">
            <caption className="my-2 text-lg font-semibold">Todo List</caption>
            <thead className="border">
                <tr>
                    <th>Id</th>
                    <th>Description</th>
                    <th>Name</th>
                    <th>Type Name</th>
                    <th>Completed</th>
                </tr>
            </thead>
            <tbody>
                {data.filter(x => x.isCompleted).map((todo) => (
                    <tr key={todo.id}>
                        <td className="text-center">{todo.id}</td>
                        <td className="text-center">{todo.description}</td>
                        <td className="text-center">{todo.name}</td>
                        <td className="text-center">{todo.typeName}</td>
                        <td className="text-center"><input type="checkbox" checked={todo.isCompleted}></input></td>
                    </tr>
                ))}
            </tbody>
        </table>

    )
}

export default CompletedTodoTable;