
// Example
var HelloWorld = React.createClass({
  render: function() {
    return <div>Hello world {this.props.Message}!</div>;
  }
});

/*

<div id="reactjavascript">
    React Javascript
</div>
<script type="text/babel">
    $(document).ready(function() {
    ReactDOM.render(
    <helloworld message=" from client side!" />,
    document.getElementById("reactjavascript")
    );
    });



</script>

*/

var SwapRequestNotification = React.createClass({
	render: function() {
	    
	    return (
            <div className="swap-request-notification notification-entry" data-id={this.props.SwapRequestGuid}>
                <div className="title">
                    Swap Request
                </div>
                <div className="swap-request-close" onClick={this.btnSwapRequestMarkAsRead}>
                    <span className="fa fa-close"></span>
                </div>
                <div className="message">
                    <a href={"/User/"+this.props.RequestorUsername+"/Profile"} className="requestor">
                        {this.props.RequestorFullName}&nbsp;
                    </a>
                    <span>
                       want to offer you his
                    </span>
                    <a href={"/SwapItem/Show?guid="+this.props.SwapItemOfferGuid} className="swap-item-offer">
                        &nbsp;{this.props.SwapItemOfferName}&nbsp;
                    </a>
                    <span>
                        and additional
                    </span>
                    <span className="money-offer">
                         &nbsp;200&nbsp;dollars&nbsp;
                    </span>
                    <span>
                        for your
                    </span>
                    <a href={"/SwapItem/Show?guid="+this.props.SwapItemGuid} className="swap-item">
                         &nbsp;{this.props.SwapItemName}&nbsp;
                    </a>
                </div>
                    <div className="swap-request-actions">
                        <button className="btn-swap-request-action btn-swap-request-accept-action" onClick={this.btnSwapRequestAcceptClick}>
                            Accept
                        </button>
                        <span>&#8226;</span>
                        <button className="btn-swap-request-action btn-swap-request-decline-action" onClick={this.btnSwapRequestDeclineClick}>
                            Decline
                        </button>
                    </div>
               
            </div>
        );
	},
	btnSwapRequestMarkAsRead: function(event) {
	    var notificationEntry = $(event.currentTarget).parents(".notification-entry").eq(0);
        var swapRequestGuid = notificationEntry.attr("data-id");
        $.ajax({
            method: "POST",
            url: "/SwapRequest/MarkAsRead",
            data: "swapRequestGuid="+swapRequestGuid,
            success: function(result, status, xhr) {
                console.log(result);
                notificationEntry.remove();
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    },
	btnSwapRequestAcceptClick: function(event) {
	    var notificationEntry = $(event.currentTarget).parents(".notification-entry").eq(0);
	    var swapRequestGuid = notificationEntry.attr("data-id");
        $.ajax({
            method: "POST",
            url: "/SwapRequest/Accept",
            data: "swapRequestGuid="+swapRequestGuid,
            success: function(result, status, xhr) {
                console.log(result);
                //notificationEntry.remove();
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    },
	btnSwapRequestDeclineClick: function(event) {
	    var notificationEntry = $(event.currentTarget).parents(".notification-entry").eq(0);
	    var swapRequestGuid = notificationEntry.attr("data-id");
        $.ajax({
            method: "POST",
            url: "/SwapRequest/Decline",
            data: "swapRequestGuid="+swapRequestGuid,
            success: function(result, status, xhr) {
                console.log(result);
                //notificationEntry.remove();
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    }
});
