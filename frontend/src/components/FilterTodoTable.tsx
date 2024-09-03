import { useState } from "react";
import AuthTodoTable from "./AuthTodoTable";
import TodoSelectionTable from "./TodoSelectionTable";
import CompletedTodoTable from "./CompletedTodoTable";
import FormTodo from "./FormTodo";

type FilterTodoTableStates = 'create' | 'assigned' | 'completed' | 'filter' | 'all';

interface FilterTodoTableProps {
    token?: string;
}
// Helpers


function renderTableContent(state: FilterTodoTableStates, token: string) {
    switch (state) {
        case 'assigned':
            return <AuthTodoTable token={token} />
        case 'all':
            return <TodoSelectionTable />
        case 'completed':
            return <CompletedTodoTable />
        case 'create':
            return <div className="w-2/3 mx-auto"><FormTodo /></div>

    }
    return (<div className="px-2">test</div>)
}


function FilterTodoTable({ token }: FilterTodoTableProps) {
    const [tableState, setTableState] = useState<FilterTodoTableStates>();
    return (
        <div>
            <div className="border text-xs h-8 flex justify-between items-center">
                <div onClick={() => setTableState("create")} className="px-2">Create new Todo</div>
                <div onClick={() => setTableState("assigned")} className="px-2">Show assigned</div>
                <div onClick={() => setTableState("completed")} className="px-2">Show Completed</div>
                <div onClick={() => setTableState("filter")} className="px-2">Show Type filter</div>
                <div onClick={() => setTableState("all")} className="px-2">Show all</div>
            </div>
            {renderTableContent(tableState, token)}
        </div>
    )
}

export default FilterTodoTable;