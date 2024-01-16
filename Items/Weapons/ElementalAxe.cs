using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ElementalAxe : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/ElementalAxe");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Elemental Axe");
        Item.damage = 400;
        Item.DamageType = DamageClass.Summon;
        Item.mana = 10;
        Item.width = 36;
        Item.height = 36;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.useStyle = 1;
        ////Tooltip.SetDefault("Summons an elemental axe to fight for you");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5f;
        Item.buffType = Mod.Find<ModBuff>("ElementalAxe").Type;
        Item.buffTime = 3600;
        Item.value = 10000000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item44;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ElementalAxeG").Type;
        Item.shootSpeed = 10f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BarofLife", 5);
        recipe.AddIngredient(null, "GalacticaSingularity", 5);
        recipe.AddIngredient(null, "InfernaCutter");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	int i = Main.myPlayer;
		float num72 = Item.shootSpeed;
		int num73 = Item.damage;
		float num74 = Item.knockBack;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
		Vector2 value = Vector2.UnitX.RotatedBy((double)player.fullRotation, default(Vector2));
		Vector2 vector3 = Main.MouseWorld - vector2;
    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
		if (player.gravDir == -1f)
		{
			num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
		}
		float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
		float num81 = num80;
		if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
		{
			num78 = (float)player.direction;
			num79 = 0f;
			num80 = num72;
		}
		else
		{
			num80 = num72 / num80;
		}
    	num78 = 0f;
		num79 = 0f;
		vector2.X = (float)Main.mouseX + Main.screenPosition.X;
		vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
		int num82 = Main.rand.Next(7);
		if (num82 == 0)
		{
			num82 = Mod.Find<ModProjectile>("ElementalAxeR").Type;
		}
		else if (num82 == 1)
		{
			num82 = Mod.Find<ModProjectile>("ElementalAxeO").Type;
		}
		else if (num82 == 2)
		{
			num82 = Mod.Find<ModProjectile>("ElementalAxeY").Type;
		}
		else if (num82 == 3)
		{
			num82 = Mod.Find<ModProjectile>("ElementalAxeG").Type;
		}
		else if (num82 == 4)
		{
			num82 = Mod.Find<ModProjectile>("ElementalAxeB").Type;
		}
		else if (num82 == 5)
		{
			num82 = Mod.Find<ModProjectile>("ElementalAxeI").Type;
		}
		else
		{
			num82 = Mod.Find<ModProjectile>("ElementalAxeV").Type;
		}
		Projectile.NewProjectile(source, vector2.X, vector2.Y, num78, num79, num82, num73, num74, i, 0f, 0f);
		return false;
    }
}}