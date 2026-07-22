import { useEffect, useRef, useState } from "react";
import Modal, { type DialogCaller } from "./Modal";


interface ExternalFunctions { showErrorMessage?: (message: string) => Promise<void>; }

export const externals: ExternalFunctions = {};
let dismiss: VoidFunction;

export default function ErrorMessage() {
  const [errorMessage, setErrorMessage] = useState<string>();
  const dialog = useRef<DialogCaller<void>>(null);
  useEffect(() => {
    externals.showErrorMessage = (message) => {
      setErrorMessage(message);
      return dialog.current?.open(resolve => dismiss = resolve);
    }
  }, []);

  return (
    <Modal className="main-panel error-message" callerRef={dialog}>
      <p>{errorMessage}</p>
      <button onClick={() => dismiss()}>OK</button>
    </Modal>
  );
}

export let showErrorMessage: (message: string) => Promise<void>;