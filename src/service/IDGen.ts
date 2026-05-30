const map = new WeakMap<object, number>();
let ID = 0;

export function getUniqueID(obj: object): number {
  if (!map.has(obj)) map.set(obj, ID++);
  return map.get(obj);
}