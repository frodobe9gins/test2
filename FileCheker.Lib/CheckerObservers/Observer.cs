namespace FileCheker.Lib
{
    /// <summary>
    /// Абстрактный клас подписчика
    /// </summary>
    abstract class Observer : IObserver
    {
        /// <summary>
        /// первичная обработка происходит тут
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void Notify(object sender, FileEventArgs e);


        /// <summary>
        /// подписка происходит из подписчика на установленную функцию субъекта
        /// </summary>
        /// <param name="provider"></param>
        public void Subscribe(IProvider provider)
        {
            provider.Notify+=Notify;
        }

        public void UnSubscribe(IProvider provider)
        {
            provider.Notify -= Notify;
        }
    }

}
