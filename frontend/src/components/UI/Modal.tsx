import React from 'react';
import { TodoExampleDTO } from 'services/backend/client.generated';
import {Button} from './Button';
import useAssign from 'hooks/useAssign';
interface ModalProps {
    onClose?: () => void;
    selectedTodo: TodoExampleDTO
}
function Modal({ onClose, selectedTodo }: ModalProps) {
    const {mutateAsync} = useAssign();
    return (
        <div className="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div className="p-8 border w-96 shadow-lg rounded-md bg-white">
                <div className="text-center">
                    <h3 className="text-2xl font-bold text-gray-900">{selectedTodo.name}</h3>
                    <div className="mt-2 px-7 py-3">
                        <p className="text-lg text-gray-500">State : {selectedTodo.isCompleted ? "Task is completed" : "Task is still pending completion"}</p>
                        <p className="text-lg text-gray-500">Todo Type Name: {selectedTodo.typeName}</p>
                        <p className="text-lg text-gray-500">Id: {selectedTodo.id}</p>
                        <Button onClick={() => {mutateAsync(selectedTodo.id)}}>Assign </Button>
                    </div>
                    <div className="flex justify-between mt-4">
                        <button onClick={onClose}>Close</button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Modal;
