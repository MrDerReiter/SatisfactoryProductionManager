import ImageButton from "./ImageButton";


interface ProductionButtonProps {
  product: string;
  size: number;
  isActive: boolean;
  callback?: VoidFunction;
}

export default function ProductionButton(props: ProductionButtonProps) {
  const { product, size, isActive, callback } = props;
  return (
    <ImageButton {...{ size, callback }}
      image={`assets/Resources/${product}.png`}
      className={"production-button" + (isActive ? " highlight" : "")} />
  );
}