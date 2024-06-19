using System;
using GUtilsUnity.Di.Builder;
using GUtilsUnity.Di.Container;
using GUtilsUnity.Features.AlertUi.Interactor;
using GUtilsUnity.Notifying.Notifications;
using GUtilsUnity.Predicates;

namespace GUtilsUnity.Features.AlertUi.Extensions
{
    public static class AlertUiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkAlertUiToNotification<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IListenNotificationGroup> getNotification,
            Func<IAlertUiInteractor> getAlertUi
        )
        {
            return LinkAlertUiToNotification(
                actionBuilder,
                getNotification,
                getAlertUi,
                _ => AlwaysPredicate.Instance
            );
        }

        public static IDiBindingActionBuilder<T> LinkAlertUiToNotification<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<IDiResolveContainer, IListenNotificationGroup> getNotification,
            Func<IAlertUiInteractor> getAlertUi,
            Func<IDiResolveContainer, IPredicate> getShowCondition
        )
        {
            IListenNotificationGroup notificationGroup = null;
            IAlertUiInteractor alertUiInteractor = null;
            IPredicate showPredicate = null;

            void RaisedStateChanged(bool state)
            {
                if (state)
                {
                    bool canShow = showPredicate.IsSatisfied();

                    if (!canShow)
                    {
                        return;
                    }

                    alertUiInteractor.SetVisible(true);
                    alertUiInteractor.PlayAlert();
                }
                else
                {
                    alertUiInteractor.SetVisible(false);
                }
            }

            actionBuilder.WhenInit((c, _) =>
            {
                notificationGroup = getNotification.Invoke(c);
                alertUiInteractor = getAlertUi.Invoke();
                showPredicate = getShowCondition.Invoke(c);

                notificationGroup.AnyRaisedStateChangedEvent.AddListener(RaisedStateChanged);

                RaisedStateChanged(notificationGroup.AnyRaised);
            });

            actionBuilder.WhenDispose(() =>
            {
                notificationGroup.AnyRaisedStateChangedEvent.RemoveListener(RaisedStateChanged);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiContainerBuilder LinkAlertUiToNotification(
            this IDiContainerBuilder actionBuilder,
            Func<IDiResolveContainer, IListenNotificationGroup> getNotification,
            Func<IDiResolveContainer, IAlertUiInteractor> getAlertUi
        )
        {
            return LinkAlertUiToNotification(
                actionBuilder,
                getNotification,
                getAlertUi,
                _ => AlwaysPredicate.Instance
            );
        }

        public static IDiContainerBuilder LinkAlertUiToNotification(
            this IDiContainerBuilder actionBuilder,
            Func<IDiResolveContainer, IListenNotificationGroup> getNotification,
            Func<IDiResolveContainer, IAlertUiInteractor> getAlertUi,
            Func<IDiResolveContainer, IPredicate> getShowCondition
        )
        {
            IListenNotificationGroup notificationGroup = null;
            IAlertUiInteractor alertUiInteractor = null;
            IPredicate showPredicate = null;

            void RaisedStateChanged(bool state)
            {
                if (state)
                {
                    bool canShow = showPredicate.IsSatisfied();

                    if (!canShow)
                    {
                        return;
                    }

                    alertUiInteractor.SetVisible(true);
                    alertUiInteractor.PlayAlert();
                }
                else
                {
                    alertUiInteractor.SetVisible(false);
                }
            }

            actionBuilder.WhenInit(c =>
            {
                notificationGroup = getNotification.Invoke(c);
                alertUiInteractor = getAlertUi.Invoke(c);
                showPredicate = getShowCondition.Invoke(c);

                notificationGroup.AnyRaisedStateChangedEvent.AddListener(RaisedStateChanged);

                RaisedStateChanged(notificationGroup.AnyRaised);
            });

            actionBuilder.WhenDispose(() =>
            {
                notificationGroup.AnyRaisedStateChangedEvent.RemoveListener(RaisedStateChanged);
            });

            return actionBuilder;
        }
    }
}
