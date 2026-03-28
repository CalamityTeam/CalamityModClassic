using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class MarkedMagnum : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Marked Magnum");
			// Tooltip.SetDefault("Shots reduce enemy protection\nProjectile damage is multiplied by all of your damage bonuses");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 3;
	        Item.width = 54;
	        Item.height = 20;
	        Item.useTime = 15;
	        Item.useAnimation = 15;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
	        Item.UseSound = SoundID.Item33;
	        Item.autoReuse = false;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("MarkRound").Type;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	float damageMult = player.GetDamage(DamageClass.Melee).Additive + player.GetDamage(DamageClass.Ranged).Additive + player.GetDamage(DamageClass.Magic).Additive + 
                CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage + player.GetDamage(DamageClass.Summon).Additive;
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * damageMult), knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.HellstoneBar, 7);
	        recipe.AddIngredient(ItemID.Obsidian, 15);
	        recipe.AddIngredient(ItemID.GlowingMushroom, 15);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}