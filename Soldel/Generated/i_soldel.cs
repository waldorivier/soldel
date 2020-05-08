namespace mupeModel {
     public interface i_soldel {
        void add_child(object child);
        bool can_add_child(object child);
        bool can_remove_me();
        void remove_me();
        bool can_update();
        void update();
        bool is_persistant();
        i_soldel copy();
    }
}