using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Ravager
{
	public class Hematemesis : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hematemesis");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 125;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 14;
            Item.rare = 8;
	        Item.width = 48;
	        Item.height = 54;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.75f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.UseSound = SoundID.Item21;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BloodBlast").Type;
	        Item.shootSpeed = 10f;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
            for (int x = 0; x < 10; x++)
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null),player.position.X + (float)Main.rand.Next(-150, 150), player.position.Y + 600f, 0f, -10f, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }
            return false;
		}
	}
}