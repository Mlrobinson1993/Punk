let currentToastId = 0;

const toasts: Ref<
  {
    id: number;
    type: "success" | "error" | "info";
    message: string;
    duration: number;
  }[]
> = ref([]);

export function useToast() {
  const maxToasts = 3;

  function showToast(
    type: "success" | "error" | "info",
    message: string,
    duration = 3000
  ) {
    if (toasts.value.length >= maxToasts) {
      toasts.value.shift();
    }

    const toastId = currentToastId++;
    const newToast = { id: toastId, message, duration, type };
    toasts.value = [...toasts.value, newToast];

    setTimeout(() => {
      toasts.value = toasts.value.filter((t) => t.id !== toastId);
    }, duration);
  }

  function removeToast(toastId: number) {
    toasts.value = toasts.value.filter((t) => t.id !== toastId);
  }

  return { toasts, showToast, removeToast };
}
