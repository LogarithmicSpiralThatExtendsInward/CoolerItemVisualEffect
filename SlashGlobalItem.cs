using MonoMod.Cil;
using System;
using Terraria.ID;

namespace CoolerItemVisualEffect
{
    public class SlashGlobalItem : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            bool useSlashEffect = item.useStyle == ItemUseStyleID.Swing && item.DamageType == DamageClass.Melee;
            if (ConfigurationSwoosh.instance.ToolsNoUseNewSwooshEffect)
            {
                useSlashEffect = useSlashEffect && item.axe == 0 && item.hammer == 0 && item.pick == 0;
            }
            return lateInstantiation && useSlashEffect;
        }

        public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            var modPlayer = player.GetModPlayer<WeaponDisplayPlayer>();
            if (!ConfigurationPreInstall.instance.UseHitbox)
                return;

            Vector2 hitboxpos = modPlayer.HitboxPosition;
            Vector2 hitboxSize = new Vector2(Math.Abs(hitboxpos.X), Math.Abs(hitboxpos.Y));
            hitbox.Width = (int)hitboxSize.X;
            hitbox.Height = (int)hitboxSize.Y;
            hitbox.X = (int)player.Center.X;
            if (hitboxpos.Y < -4)
            {
                hitbox.Y = (int)player.Center.Y - (int)hitboxSize.Y;
                if (player.gravDir == -1f)
                {
                    hitbox.Y += (int)hitboxSize.Y;
                }
            }
            else if (hitboxpos.Y > 4)
            {
                hitbox.Y = (int)player.Center.Y;
                if (player.gravDir == -1f)
                {
                    hitbox.Y -= (int)hitboxSize.Y;
                }
            }
            else
            {
                hitbox.Y = (int)player.Center.Y - 4;
                hitbox.Height = 8;
            }
            if (hitboxpos.X < -4)
            {
                hitbox.X -= (int)hitboxSize.X;
            }
            else if (hitboxpos.X > 4)
            {
            }
            else
            {
                hitbox.X = (int)player.Center.X - 4;
                hitbox.Width = 8;
            }
            //Texture2D texture2D = TextureAssets.MagicPixel.Value;
            //Color color = Color.Blue;
            //Vector2 Pos = new Vector2(hitbox.X, hitbox.Y);
            //Main.spriteBatch.Draw(texture2D, Pos + new Vector2(hitbox.Width * 0.5f, 0) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, 0, new Vector2(0.5f, 0.5f), new Vector2(hitbox.Width, 2f), SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(texture2D, Pos + new Vector2(hitbox.Width, hitbox.Height * 0.5f) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, 0, new Vector2(0.5f, 0.5f), new Vector2(2f, hitbox.Height), SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(texture2D, Pos + new Vector2(hitbox.Width * 0.5f, hitbox.Height) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, 0, new Vector2(0.5f, 0.5f), new Vector2(hitbox.Width, 2f), SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(texture2D, Pos + new Vector2(0, hitbox.Height * 0.5f) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, 0, new Vector2(0.5f, 0.5f), new Vector2(2f, hitbox.Height), SpriteEffects.None, 0f);
            // ?????????????????????????????????????????????????????????
            base.UseItemHitbox(item, player, ref hitbox, ref noHitbox);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            ////WeaponDisplayPlayer modPlayer = player.GetModPlayer<WeaponDisplayPlayer>();
            ////Main.NewText(player.GetModPlayer<WeaponDisplayPlayer>().UseSlash);
            ////Main.NewText("!!!!!!");
            //if (Main.myPlayer == player.whoAmI) // && player.GetModPlayer<WeaponDisplayPlayer>().UseSlash
            //{
            //    WeaponDisplay.ChangeShooshStyle(player);
            //    //var vec = Main.MouseWorld - player.Center;
            //    //vec.Y *= player.gravDir;
            //    //player.direction = Math.Sign(vec.X);
            //    //modPlayer.negativeDir ^= true;
            //    //modPlayer.rotationForShadow = vec.ToRotation() + Main.rand.NextFloat(-MathHelper.Pi / 6, MathHelper.Pi / 6);
            //    //modPlayer.kValue = Main.rand.NextFloat(1, 2);
            //    //modPlayer.UseSlash = ConfigurationSwoosh.instance.CoolerSwooshActive;
            //    //if (Main.netMode == NetmodeID.MultiplayerClient) {
            //    //    ModPacket packet = Mod.GetPacket();
            //    //    packet.Write((byte)HandleNetwork.MessageType.BasicStats);
            //    //    packet.Write(modPlayer.negativeDir);
            //    //    packet.Write(modPlayer.rotationForShadow);
            //    //    packet.Write(modPlayer.kValue);
            //    //    packet.Write(modPlayer.UseSlash);
            //    //    packet.Send(-1, -1); // ????????????????????? ???????????????????????????????????????
            //    //    NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // ??????direction
            //    //}
            //}
            return true;
        }

        // UseStyle????????????SetCompositeArmFront?????????????????????????????????Draw??????????????????u, v???????????????????????????????????????????????????????????????
        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            //Main.NewText(item.type);
            //WeaponDisplayPlayer modPlayer = player.GetModPlayer<WeaponDisplayPlayer>();
            ////Main.NewText(item.type);
            //if (modPlayer.UseSlash) //
            //{
            //    //var fac = modPlayer.factorGeter;
            //    //fac = !modPlayer.negativeDir ? 1 - fac : fac;
            //    //var theta = (fac * -1.125f + (1 - fac) * 0.1125f) * MathHelper.Pi;//??????????????????????????????????????????????????????????????????????????????????????????
            //    //float xScaler = modPlayer.kValue;//??????x?????????????????????
            //    //float scaler = item.Size.Length() * item.scale / xScaler * 0.7f;//???????????????????????????(???????????????????????????????????????????????????????????????
            //    //var cos = (float)Math.Cos(theta) * scaler;
            //    //var sin = (float)Math.Sin(theta) * scaler;//??????(cos,sin)?????????????????????????????????????????????????????????????????????(0,0)????????????????????????????????????????????????
            //    //var u = new Vector2(xScaler * (cos - sin), -cos - sin).RotatedBy(modPlayer.rotationForShadow);
            //    //var v = new Vector2(-xScaler * (cos + sin), sin - cos).RotatedBy(modPlayer.rotationForShadow);//???????????????????????????????????????????????????scaler??????0.7??????0.5

            //    //var vel = u + v;
            //    //drawPlayer.bodyFrame.Y = 112 + 56 * (int)(Math.Abs(new Vector2(-vel.Y, vel.X).ToRotation()) / MathHelper.Pi * 3);
            //    player.itemRotation = modPlayer.direct - MathHelper.ToRadians(90f); // ????????????-90????????re???
            //    player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, player.itemRotation);
            //}
            base.UseStyle(item, player, heldItemFrame);
        }

        //public override void Load()
        //{
        //    IL.Terraria.Player.ItemCheck_MeleeHitNPCs += Player_ItemCheck_MeleeHitNPCs;
        //}

        //private void Player_ItemCheck_MeleeHitNPCs(ILContext il)
        //{
        //    var c = new ILCursor(il);
        //    while (c.TryGotoNext(MoveType.After, i => i.MatchLdcR8(0.33)))
        //    {
        //        c.EmitDelegate<Func<double, double>>((_) =>
        //        {
        //            return 1.0 / CoolerItemVisualEffect.ConfigGameplay.ItemAttackCD;
        //        });
        //    }
        //}
    }
    public class MYGBI : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            bool useSlashEffect = item.useStyle == ItemUseStyleID.Swing && item.DamageType == DamageClass.Melee;
            if (ConfigurationSwoosh.instance.ToolsNoUseNewSwooshEffect)
            {
                useSlashEffect = useSlashEffect && item.axe == 0 && item.hammer == 0 && item.pick == 0;
            }
            return lateInstantiation && useSlashEffect;
        }

        public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            var modPlayer = player.GetModPlayer<WeaponDisplayPlayer>();
            if (!ConfigurationPreInstall.instance.UseHitbox)
                return;

            Vector2 hitboxpos = modPlayer.HitboxPosition;
            Vector2 hitboxSize = new Vector2(Math.Abs(hitboxpos.X), Math.Abs(hitboxpos.Y));
            hitbox.Width = (int)hitboxSize.X;
            hitbox.Height = (int)hitboxSize.Y;
            hitbox.X = (int)player.Center.X;
            if (hitboxpos.Y < -4)
            {
                hitbox.Y = (int)player.Center.Y - (int)hitboxSize.Y;
                if (player.gravDir == -1f)
                {
                    hitbox.Y += (int)hitboxSize.Y;
                }
            }
            else if (hitboxpos.Y > 4)
            {
                hitbox.Y = (int)player.Center.Y;
                if (player.gravDir == -1f)
                {
                    hitbox.Y -= (int)hitboxSize.Y;
                }
            }
            else
            {
                hitbox.Y = (int)player.Center.Y - 4;
                hitbox.Height = 8;
            }
            if (hitboxpos.X < -4)
            {
                hitbox.X -= (int)hitboxSize.X;
            }
            else if (hitboxpos.X > 4)
            {
            }
            else
            {
                hitbox.X = (int)player.Center.X - 4;
                hitbox.Width = 8;
            }
            //Texture2D texture2D = TextureAssets.MagicPixel.Value;
            //Color color = Color.Blue;
            //Vector2 Pos = new Vector2(hitbox.X, hitbox.Y);
            //Main.spriteBatch.Draw(texture2D, Pos + new Vector2(hitbox.Width * 0.5f, 0) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, 0, new Vector2(0.5f, 0.5f), new Vector2(hitbox.Width, 2f), SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(texture2D, Pos + new Vector2(hitbox.Width, hitbox.Height * 0.5f) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, 0, new Vector2(0.5f, 0.5f), new Vector2(2f, hitbox.Height), SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(texture2D, Pos + new Vector2(hitbox.Width * 0.5f, hitbox.Height) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, 0, new Vector2(0.5f, 0.5f), new Vector2(hitbox.Width, 2f), SpriteEffects.None, 0f);
            //Main.spriteBatch.Draw(texture2D, Pos + new Vector2(0, hitbox.Height * 0.5f) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 1, 1)), color, 0, new Vector2(0.5f, 0.5f), new Vector2(2f, hitbox.Height), SpriteEffects.None, 0f);
            // ?????????????????????????????????????????????????????????
            base.UseItemHitbox(item, player, ref hitbox, ref noHitbox);
        }
        public override bool CanUseItem(Item item, Player player)
        {
            //Main.NewText(("CanUseItem", item.type));
            //int a = ItemID.AaronsBreastplate;
            return base.CanUseItem(item, player);
        }
        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            WeaponDisplayPlayer modPlayer = player.GetModPlayer<WeaponDisplayPlayer>();
            if (modPlayer.UseSlash) //
            {
                player.itemRotation = modPlayer.direct - MathHelper.ToRadians(90f); // ????????????-90????????re???
                player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, player.itemRotation);
            }

            base.UseStyle(item, player, heldItemFrame);
        }
        public override void UseAnimation(Item item, Player player)
        {
            var flag = player.HeldItem.damage > 0 && player.HeldItem.useStyle == ItemUseStyleID.Swing && player.HeldItem.DamageType == DamageClass.Melee && !player.HeldItem.noUseGraphic && ConfigurationSwoosh.instance.CoolerSwooshActive;
            flag |= (player.HeldItem.type == ItemID.Zenith || player.HeldItem.type == ModContent.ItemType<Weapons.FirstFractal_CIVE>()) && ConfigurationSwoosh.instance.allowZenith && ConfigurationSwoosh.instance.CoolerSwooshActive;
            if (Main.myPlayer == player.whoAmI && flag) // 
            {
                CoolerItemVisualEffect.ChangeShooshStyle(player);
            }
            base.UseAnimation(item, player);
        }
        public override bool? UseItem(Item item, Player player)
        {
            //Main.NewText(("UseItem", item.type));

            return base.UseItem(item, player);
        }
        public override float UseAnimationMultiplier(Item item, Player player)
        {
            //Main.NewText(("UseAnimationMultiplier", item.type));

            return base.UseAnimationMultiplier(item, player);
        }
    }
}