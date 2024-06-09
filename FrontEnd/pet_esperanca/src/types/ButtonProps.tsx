export interface ButtonProps {
    text: string,
    url: string,
    type: string,
    func?: () => (void)
}