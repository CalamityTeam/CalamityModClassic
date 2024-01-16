using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class LunarianBow : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/LunarianBow");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Lunarian Bow");
        Item.damage = 25;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 22;
        Item.height = 58;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("The power of the night resides within this bow");
        Item.knockBack = 2f;
        Item.value = 300000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item75;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("LunarBolt").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 8f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num117 = 0.314159274f;
		int num118 = 2;
		Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
		vector7.Normalize();
		vector7 *= 15f;
		bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
		for (int num119 = 0; num119 < num118; num119++)
		{
			float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
			Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
			if (!flag11)
			{
				value9 -= vector7;
			}
			int num121 = Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("LunarBolt").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
			Main.projectile[num121].noDropItem = true;
		}
		return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DemonBow);
        recipe.AddIngredient(ItemID.MoltenFury);
        recipe.AddIngredient(ItemID.BeesKnees);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.TendonBow);
        recipe.AddIngredient(ItemID.MoltenFury);
        recipe.AddIngredient(ItemID.BeesKnees);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}}