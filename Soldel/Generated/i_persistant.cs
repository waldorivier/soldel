namespace mupeModel {
    internal interface i_persistant {
        void add_child(object child);
        bool can_add_child(object child);
        bool can_remove_me();
        void remove_me();
    }
}