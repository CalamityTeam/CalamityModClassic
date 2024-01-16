using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class SpectralstormCannon : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/SpectralstormCannon");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Spectralstorm Cannon");
			Item.damage = 45;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 66;
			Item.height = 26;
			Item.useTime = 3;
			////Tooltip.SetDefault("70% chance to not consume flares\nFires a storm of ectoplasm and flares");
			Item.useAnimation = 9;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 1.5f;
			Item.value = 900000;
			Item.rare = 9;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = 163; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 9.5f;
			Item.useAmmo = 931;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 70)
	    		return false;
	    	return true;
	    }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    int num6 = Main.rand.Next(1, 2);
		    for (int index = 0; index < num6; ++index)
		    {
		        float num7 = velocity.X;
		        float num8 = velocity.Y;
		        float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
		        int projectile = Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		        Main.projectile[projectile].timeLeft = 200;
		    }
		    int projectile2 = Projectile.NewProjectile(source, position, velocity,297, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    Main.projectile[projectile2].DamageType = DamageClass.Ranged;
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "FirestormCannon");
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}