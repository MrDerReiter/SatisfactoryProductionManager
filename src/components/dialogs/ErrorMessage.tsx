import { useEffect, useState } from "react";
import Modal, { type DialogCaller } from "./Modal";

interface ExternalFunctions { showErrorMessage?: (message: string) => Promise<void>; }
export const externals: ExternalFunctions = {};

const dialog: DialogCaller<void> = {};
let dismiss: VoidFunction;

export default function ErrorMessage() {
  const [errorMessage, setErrorMessage] = useState<string>();
  useEffect(() => {
    externals.showErrorMessage = (message) => {
      setErrorMessage(message);
      return dialog.open(resolve => dismiss = resolve);
    }
  }, []);

  return (
    <Modal className="main-panel error-message" caller={dialog}>
      <p>{errorMessage}</p>
      <button onClick={() => dismiss()}>OK</button>
    </Modal>
  );
}

export let showErrorMessage: (message: string) => Promise<void>;